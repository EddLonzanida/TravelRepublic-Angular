using Eml.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TravelRepublic.Api;
using TravelRepublic.Tests.Utils.BaseClasses;

namespace TravelRepublic.Tests.Utils.ClassData.Conventions
{
    public class ControllerClassData : ClassDataBase<Type>
    {
        private static List<Type> _controllers;

        public ControllerClassData()
            : base(() =>
            {
                return _controllers 
                       ?? (_controllers = typeof(Program).Assembly
                           .GetClasses(type => typeof(ControllerBase).IsAssignableFrom(type)));
            })
        { }
    }
}
