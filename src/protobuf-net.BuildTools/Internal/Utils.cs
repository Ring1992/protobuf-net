﻿using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using System.Text;

namespace ProtoBuf.BuildTools.Internal
{
    internal static class Utils
    {
        internal static ImmutableArray<DiagnosticDescriptor> GetDeclared(Type type)
        {
            var fields = type?.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            if (fields is null || fields.Length == 0) return ImmutableArray<DiagnosticDescriptor>.Empty;

            var builder = ImmutableArray.CreateBuilder<DiagnosticDescriptor>(fields.Length);
            foreach (var field in fields)
            {
                if (field.FieldType == typeof(DiagnosticDescriptor) && field.GetValue(null) is DiagnosticDescriptor descriptor)
                {
                    builder.Add(descriptor);
                }
            }
            return builder.ToImmutable();
        }
    }
}