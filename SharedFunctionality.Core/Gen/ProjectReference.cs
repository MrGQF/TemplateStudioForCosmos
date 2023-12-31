﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Microsoft.Templates.Core.Gen
{
    public class ProjectReference
    {
        public string Project { get; set; }

        public string ReferencedProject { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is ProjectReference projectReference)
            {
                return Project == projectReference.Project
                    && ReferencedProject == projectReference.ReferencedProject;
            }

            return false;
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}
