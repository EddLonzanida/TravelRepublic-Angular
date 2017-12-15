using System;using System.Collections.Generic;using Eml.Contracts.Services;using NSubstitute;using NUnit.Framework;using Shouldly;using TravelRepublic.Business.Common.Dto;using TravelRepublic.Business.Managers;using TravelRepublic.Business.RequestEngines;using TravelRepublic.Business.Requests;namespace TravelRepublic.Tests.Unit.RequestEngines
{
    [TestFixture]
    public class FlightBuilderEngineTests
    {
        private FlightBuilderEngine engine;

        private IFlightBuilder flightBuilder;

        private IClockService clockService;

        private Flight normalFlightWithTwoHourDuration;

        private Flight normalMultiSegmentFlight;

        private Flight flightDepartingInThePast;

        private Flight flightThatDepartsBeforeItArrives;

        private Flight flightWithMoreThanTwoHoursGroundTime;

        private Flight anotherFlightWithMoreThanTwoHoursGroundTime;

        [SetUp]
        public void Setup()
        {
            SetupExpectedFlights();

            var now = DateTime.Parse("2017-07-24T17:31:10.3003561+08:00");

            clockService = Substitute.For<IClockService>();
            clockService.Now().Returns(now);
            clockService.NowAddDays(3).Returns(now.AddDays(3));
            flightBuilder = new FlightBuilder(clockService);
            engine = new FlightBuilderEngine(flightBuilder, clockService);
        }

        [Test]
        public void Response_ShouldBeAll()
        {
            const eFlightFilter searchCode = eFlightFilter.None;
            var request = new FlightBuilderRequest(searchCode);

            var response = engine.Get(request);

            clockService.Received(1).NowAddDays(3);
            response.Flights.Count.ShouldBe(6);
        }

        [Test]
        public void Response_ShouldBeNormalFlightWithTwoHoursWaitingTime()
        {
            const eFlightFilter searchCode = eFlightFilter.TwoHoursWaitingTime;
            var request = new FlightBuilderRequest(searchCode);

            var response = engine.Get(request);

            var flights = response.Flights;
            var segments = flights.Flatten();
            clockService.Received(1).NowAddDays(3);
            flights.Count.ShouldBe(2);
            flightWithMoreThanTwoHoursGroundTime.Exists(segments).ShouldBeTrue();
            anotherFlightWithMoreThanTwoHoursGroundTime.Exists(segments).ShouldBeTrue();
        }

        [Test]
        public void Response_ShouldBeFlightsThatDepartsBeforeItArrives()
        {
            const eFlightFilter searchCode = eFlightFilter.ArrivalBeforeDepartureDate;
            var request = new FlightBuilderRequest(searchCode);

            var response = engine.Get(request);

            var segments = response.Flights.Flatten();
            clockService.Received(1).NowAddDays(3);
            response.Flights.Count.ShouldBe(1);
            flightThatDepartsBeforeItArrives.Exists(segments).ShouldBeTrue();
        }

        [Test]
        public void Response_ShouldBeArrivalBeforeDepartureDateTwoHoursWaitingTime()
        {
            const eFlightFilter searchCode = eFlightFilter.ArrivalBeforeDepartureDateTwoHoursWaitingTime;
            var request = new FlightBuilderRequest(searchCode);

            var response = engine.Get(request);

            var segments = response.Flights.Flatten();
            clockService.Received(1).NowAddDays(3);
            response.Flights.Count.ShouldBe(3);
            flightThatDepartsBeforeItArrives.Exists(segments).ShouldBeTrue();
            flightWithMoreThanTwoHoursGroundTime.Exists(segments).ShouldBeTrue();
            anotherFlightWithMoreThanTwoHoursGroundTime.Exists(segments).ShouldBeTrue();
        }

        [Test]
        public void Response_ShouldBeFlightsDepartingInThePast()
        {
            const eFlightFilter searchCode = eFlightFilter.DepartureBeforeCurrentDate;
            var request = new FlightBuilderRequest(searchCode);

            var response = engine.Get(request);

            var segments = response.Flights.Flatten();
            clockService.Received(1).Now();
            clockService.Received(1).NowAddDays(3);
            response.Flights.Count.ShouldBe(1);
            flightDepartingInThePast.Exists(segments).ShouldBeTrue();
        }

        [Test]
        public void Response_ShouldBeFlightsDepartingInThePastAndTwoHoursWaitingTime()
        {
            const eFlightFilter searchCode = eFlightFilter.DepartureBeforeCurrentDateTwoHoursWaitingTime;
            var request = new FlightBuilderRequest(searchCode);

            var response = engine.Get(request);

            var segments = response.Flights.Flatten();
            clockService.Received(1).Now();
            clockService.Received(1).NowAddDays(3);
            response.Flights.Count.ShouldBe(3);
            anotherFlightWithMoreThanTwoHoursGroundTime.Exists(segments).ShouldBeTrue();
            flightWithMoreThanTwoHoursGroundTime.Exists(segments).ShouldBeTrue();
            flightDepartingInThePast.Exists(segments).ShouldBeTrue();
        }

        [Test]
        public void Response_ShouldBeFlightsDepartingInThePastAndArrivalBeforeDepartureDate()
        {
            const eFlightFilter searchCode = eFlightFilter.DepartureBeforeCurrentDateArrivalBeforeDepartureDate;
            var request = new FlightBuilderRequest(searchCode);

            var response = engine.Get(request);

            var segments = response.Flights.Flatten();
            clockService.Received(1).Now();
            clockService.Received(1).NowAddDays(3);
            response.Flights.Count.ShouldBe(2);
            flightThatDepartsBeforeItArrives.Exists(segments).ShouldBeTrue();
            flightDepartingInThePast.Exists(segments).ShouldBeTrue();
        }

        [Test]
        public void Response_ShouldBeBeFlightsDepartingInThePastAndArrivalBeforeDepartureDateAndTwoHoursWaitingTime()
        {
            const eFlightFilter searchCode = eFlightFilter.DepartureBeforeCurrentDateArrivalBeforeDepartureDateTwoHoursWaitingTime;
            var request = new FlightBuilderRequest(searchCode);

            var response = engine.Get(request);

            var segments = response.Flights.Flatten();
            clockService.Received(1).Now();
            clockService.Received(1).NowAddDays(3);
            response.Flights.Count.ShouldBe(4);
            anotherFlightWithMoreThanTwoHoursGroundTime.Exists(segments).ShouldBeTrue();
            flightWithMoreThanTwoHoursGroundTime.Exists(segments).ShouldBeTrue();
            flightThatDepartsBeforeItArrives.Exists(segments).ShouldBeTrue();
            flightDepartingInThePast.Exists(segments).ShouldBeTrue();
        }

        private void SetupExpectedFlights()
        {
            normalFlightWithTwoHourDuration = new Flight
            {
                Segments = new List<Segment>
                {
                    new Segment { DepartureDate = DateTime.Parse("2017-07-27T17:31:10.3003561+08:00"), ArrivalDate = DateTime.Parse("2017-07-27T19:31:10.3003561+08:00") }
                }
            };

            normalMultiSegmentFlight = new Flight
            {
                Segments = new List<Segment>
                {
                    new Segment { DepartureDate = DateTime.Parse("2017-07-27T17:31:10.3003561+08:00"), ArrivalDate = DateTime.Parse("2017-07-27T19:31:10.3003561+08:00") },
                    new Segment { DepartureDate = DateTime.Parse("2017-07-27T20:31:10.3003561+08:00"), ArrivalDate = DateTime.Parse("2017-07-27T22:31:10.3003561+08:00") }
                }
            };

            flightDepartingInThePast = new Flight
            {
                Segments = new List<Segment>
                {
                    new Segment { DepartureDate = DateTime.Parse("2017-07-21T17:31:10.3003561+08:00"), ArrivalDate = DateTime.Parse("2017-07-27T17:31:10.3003561+08:00") }
                }
            };

            flightThatDepartsBeforeItArrives = new Flight
            {
                Segments = new List<Segment>
                {
                    new Segment { DepartureDate = DateTime.Parse("2017-07-27T17:31:10.3003561+08:00"), ArrivalDate = DateTime.Parse("2017-07-27T11:31:10.3003561+08:00") }
                }
            };

            flightWithMoreThanTwoHoursGroundTime = new Flight
            {
                Segments = new List<Segment>
                {
                    new Segment { DepartureDate = DateTime.Parse("2017-07-27T17:31:10.3003561+08:00"), ArrivalDate = DateTime.Parse("2017-07-27T19:31:10.3003561+08:00") },
                    new Segment { DepartureDate = DateTime.Parse("2017-07-27T22:31:10.3003561+08:00"), ArrivalDate = DateTime.Parse("2017-07-27T23:31:10.3003561+08:00") }
                }
            };

            anotherFlightWithMoreThanTwoHoursGroundTime = new Flight
            {
                Segments = new List<Segment>
                {
                    new Segment { DepartureDate = DateTime.Parse("2017-07-27T17:31:10.3003561+08:00"), ArrivalDate = DateTime.Parse("2017-07-27T19:31:10.3003561+08:00") },
                    new Segment { DepartureDate = DateTime.Parse("2017-07-27T20:31:10.3003561+08:00"), ArrivalDate = DateTime.Parse("2017-07-27T21:31:10.3003561+08:00") },
                    new Segment { DepartureDate = DateTime.Parse("2017-07-27T23:31:10.3003561+08:00"), ArrivalDate = DateTime.Parse("2017-07-28T00:31:10.3003561+08:00") }
                }
            };

        }
    }
}
