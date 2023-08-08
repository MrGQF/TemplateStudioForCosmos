// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.Templates.Test;
using Xunit;

namespace TemplateStudioForWPF.Tests
{
    [Trait("Group", "MinimumWPF")]
    public class CodeStyleEnforcementTests : BaseCodeStyleEnforcementTests
    {
        public override string TemplatesRoot() => TestConstants.Ts4WpfRelativeTemplatesRoot;

        [Fact]
        public void EnsureCsTemplatesDoNotUseTabsAsWhitespace()
        {
            EnsureTabsNotUsed("*.cs");
        }

        [Fact]
        public void EnsureCSharpCodeDoesNotUseLocalizedExceptionMessages()
        {
            var result = CodeDoesNotMatchRegex("(throw).*(Exception\\().*(GetLocalized)", "*.cs");

            Assert.True(result.Item1, result.Item2);

        }
    }
}
