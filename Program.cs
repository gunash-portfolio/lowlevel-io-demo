using Spectre.Console;

class Program{
    static void Main(){
        AnsiConsole.Write(new FigletText("Hello, World!").Centered());
        AnsiConsole.Write(new FigletText("First GUI").LeftJustified().Color(Color.Blue));

        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>().Title("Choose your destiny").PageSize(10).AddChoices(new[]{"Be a good person","Be a bad person","Be a neutral person, but be careful not to get caught with your own actions","If you choose to be a bad person, you will be punished by the law","If you choose to be a good person, you will be rewarded by the law","If you choose to be a neutral person, you will be left alone"}).UseConverter(choice => choice.ToUpperInvariant()));
    

    switch(choice){
        case "Be a good person":
        AnsiConsole.Write(new Markup("[green]You are a good person[/]"));
        break;
        case "Be a bad person":
        AnsiConsole.Write(new Markup("[red]You are a bad person[/]"));
        break;
        case "Be a neutral person, but be careful not to get caught with your own actions":
        AnsiConsole.Write(new Markup("[yellow]You are a neutral person, but be careful not to get caught with your own actions[/]"));
        break;
        case "If you choose to be a bad person, you will be punished by the law":
        AnsiConsole.Write(new Markup("[red]You are a bad person, and you will be punished by the law[/]"));
        break;
        case "If you choose to be a good person, you will be rewarded by the law":
        AnsiConsole.Write(new Markup("[green]You are a good person, and you will be rewarded by the law[/]"));
        break;
        case "If you choose to be a neutral person, you will be left alone":
        AnsiConsole.Write(new Markup("[yellow]You are a neutral person, and you will be left alone[/]"));
        break;
        default:
        AnsiConsole.Write(new Markup("[red]Invalid choice[/]"));
        break;
    }
    }
}