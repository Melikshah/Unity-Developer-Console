﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Console {

    public interface ICommand {


        ConsoleOutput Logic();


    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ConsoleCommandAttribute : Attribute {
        public string queryIdentity; //Like set,get,execute
        public string description;

        public ConsoleCommandAttribute(string _queryIdentity, string _description)
        {
            queryIdentity = _queryIdentity;
            description = _description;
        }

    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class CommandParameterAttribute : Attribute
    {
        public string description;


        public CommandParameterAttribute(string _description)
        {
            description = _description;
        }

    }

    public class Command : ICommand
    {
        public Dictionary<string, CommandParameter> commandParameters = new Dictionary<string, CommandParameter>();

        public virtual ConsoleOutput Logic() //Logic for execute, returns the output
        {
            return new ConsoleOutput("", ConsoleOutput.OutputType.Log);
        }

        public string GetDescription()
        {
           return ((ConsoleCommandAttribute)Attribute.GetCustomAttribute(this.GetType(), typeof(ConsoleCommandAttribute))).description;
        }
        public string GetQueryIdentity()
        {
            return ((ConsoleCommandAttribute)Attribute.GetCustomAttribute(this.GetType(), typeof(ConsoleCommandAttribute))).queryIdentity;
        }
    }

    public abstract class CommandParameter
    {
        public object optionParameter;
        public Type genericType;
    }

    public class CommandParameter<TOption> : CommandParameter
    {
        public TOption optionParameter
        {
            get {
                if (base.optionParameter == null)
                {
                    return default;
                }
                return (TOption)base.optionParameter;

            }
        }
        public CommandParameter()
        {
            base.genericType = typeof(TOption);
            base.optionParameter = null;
        }

    }
    
}