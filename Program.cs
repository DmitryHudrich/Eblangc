// Eblang (при поддержке - 👉🏻 Егора👈🏿, Химеры 😍, гучи💰,  Данька 🦷,
// чела с очень длинным имеем смокинг там что-то там и трансом на Аве, 
// void и многих других)
// Copyright (c) 2024 W1thluv. All Rights Reserved.

/*
 *  ACTUALLY LANG GRAMMAR:
 *
 *      expression      → literal
 *                      | unary
 *                      | binary
 *                      | grouping ;
 *
 *      literal         → NUMBER | STRING | "true" | "false" | "nil" ;
 *      grouping        → "(" expression ")" ;
 *      unary           → ( "-" | "!" ) expression ;
 *      binary          → expression operator expression ;
 *      operator        → "==" | "!=" | "<" | "<=" | ">" | ">="
 *                      | "+"  | "-"  | "*" | "/" ;
 *
 *
 *  I guess this should not be there.
 */

using Eblangc;

if (args.Length > 1) {
    Console.WriteLine("Usage: eblangc [script]");
    Environment.Exit(64);
}
else if (args.Length == 1) {
    // Eblang.RunFile(args[0]);
}
else {
    Eblang.RunPrompt();
}

internal static class Eblang {
    public static void RunPrompt() {
        while (true) {
            Console.Write("> ");
            var line = Console.ReadLine();
            if (line == null || String.IsNullOrWhiteSpace(line)) {
                break;
            }
            Run(line);
            ErrorHandler.HadError = false;
        }
    }

    private static void Run(String source) {
        var scanner = new Scanner(source);
        var tokens = scanner.ScanTokens();

        foreach (var token in tokens) {
            Console.WriteLine($"Token [{token}]");
        }
    }

    public static void RunFile(String v) {
        throw new NotImplementedException();
    }
}

internal static class ErrorHandler {
    public static Boolean HadError { get; set; } = false;

    public static void Error(Int32 line, String message) {
        Report(line, "", message);
    }

    public static void Report(Int32 line, String where, String message) {
        Console.WriteLine(
            "[line " + line + "] Error" + where + ": " + message);
        HadError = true;
    }
}

