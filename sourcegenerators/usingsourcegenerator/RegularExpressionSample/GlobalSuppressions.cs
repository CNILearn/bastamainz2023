// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Design", "CA1050:Declare types in namespaces", Justification = "Just a small sample", Scope = "module")]
[assembly: SuppressMessage("GeneratedRegex", "SYSLIB1045:Convert to 'GeneratedRegexAttribute'.", Justification = "Sample showing differences with generated regex", Scope = "member", Target = "~F:TestIt.s_abcOrDef")]
[assembly: SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Benchmark requires instance members", Scope = "member", Target = "~M:TestIt.TestWithReflection")]
[assembly: SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Benchmark requires instance members", Scope = "member", Target = "~M:TestIt.TestWithGenerator")]
