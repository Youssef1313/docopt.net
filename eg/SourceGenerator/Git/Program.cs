#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using DocoptNet;
using Git;

try
{
    return Run(args);
}
catch (DocoptExitException e)
{
    Console.WriteLine(e.Message);
    return e.ErrorCode;
}
catch (Exception e)
{
    Console.Error.WriteLine(e);
    return 0xbd;
}

static int Run(string[] args)
{
    var arguments = GitArguments.Apply(args, version: "git version 1.7.4.4", optionsFirst: true);

    Console.WriteLine("global arguments:");
    foreach (var (name, value) in arguments)
        Console.WriteLine($"{name} = {value}");

    Console.WriteLine("command arguments:");

    IEnumerable<KeyValuePair<string, object?>> Apply(Func<IEnumerable<string>, IEnumerable<KeyValuePair<string, object?>>> f)
    {
        var argv = new[] { arguments.ArgCommand! }.Concat(arguments.ArgArgs).ToArray();
        return f(argv);
    }

    var commandArgs = arguments.ArgCommand switch
    {
        "add"       => Apply(args => GitAddArguments.Apply(args)),
        "branch"    => Apply(args => GitBranchArguments.Apply(args)),
        "checkout"  => Apply(args => GitCheckoutArguments.Apply(args)),
        "clone"     => Apply(args => GitCloneArguments.Apply(args)),
        "commit"    => Apply(args => GitCommitArguments.Apply(args)),
        "push"      => Apply(args => GitPushArguments.Apply(args)),
        "remote"    => Apply(args => GitRemoteArguments.Apply(args)),
        var command => throw new ApplicationException($"{command} is not a Git command. See 'git help'.")
    };

    foreach (var (name, value) in commandArgs)
        Console.WriteLine($"{name} = {value}");

    return 0;
}
