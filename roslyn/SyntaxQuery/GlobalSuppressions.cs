// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "This program queries for lowercase methods and properties", Scope = "module")]
[assembly: SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Needed for syntax query", Scope = "module")]
[assembly: SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Emtpy methods used for syntax query", Scope = "module")]
