﻿// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License.  See License.txt in the project root for license information.

namespace Community.OData.Linq.OData
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;

    /// <summary>
    /// Allows client to tell OData which are the custom aggregation methods defined.
    /// In order to do it, it must receive a methodToken - that is the full identifier
    /// of the method in the OData URL - and an IDictionary that maps the input type
    /// of the aggregation method to its MethodInfo.
    /// </summary>
    public class CustomAggregateMethodAnnotation
    {
        private readonly Dictionary<string, IDictionary<Type, MethodInfo>> _tokenToMethodMap
            = new Dictionary<string, IDictionary<Type, MethodInfo>>();

        /// <summary>
        /// Get an implementation of a method with the specifies returnType and methodToken.
        /// If there's no method that matches the requirements, returns null.
        /// </summary>
        /// <param name="methodToken">The given method token.</param>
        /// <param name="returnType">The given return type.</param>
        /// <param name="methodInfo">The output of method info.</param>
        /// <returns>True if the method info was found, false otherwise.</returns>
        [SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", Justification = "Out param is appropriate here")]
        public bool GetMethodInfo(string methodToken, Type returnType, out MethodInfo methodInfo)
        {
            IDictionary<Type, MethodInfo> methodWrapper;
            methodInfo = null;

            if (this._tokenToMethodMap.TryGetValue(methodToken, out methodWrapper))
            {
                return methodWrapper.TryGetValue(returnType, out methodInfo);
            }

            return false;
        }
    }
}
