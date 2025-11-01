# MyFirstApp - Interactive Console GUI

A .NET 8.0 console application featuring a beautiful terminal-based user interface built with Spectre.Console, demonstrating modern console UI capabilities with interactive menus and styled text output.

## Project Overview

This project showcases the power of Spectre.Console library to create rich, interactive terminal applications with:
- ASCII art banner text (FigletText)
- Colored and styled text output
- Interactive selection prompts
- Modern terminal UI/UX

## Features

### Visual Elements
- **FigletText Banners**: Large ASCII art text for eye-catching headers
- **Centered and Left-Justified Text**: Flexible text alignment options
- **Color Support**: Blue, green, red, and yellow text styling
- **Markup Language**: Rich text formatting with Spectre.Console markup syntax

### Interactive Menu
An interactive selection prompt allowing users to choose their character alignment:
- Be a good person
- Be a bad person
- Be a neutral person
- Multiple outcome scenarios with conditional feedback

The application responds to user choices with appropriately colored feedback messages.

## Technology Stack

- **.NET 8.0** - Latest LTS version of .NET
- **C#** - Modern C# with implicit usings enabled
- **Spectre.Console 0.53.0** - Terminal UI library for rich console applications

## Getting Started

### Prerequisites

- **.NET 8.0 SDK** or later
- A modern terminal with ANSI color support (macOS Terminal, iTerm2, Windows Terminal, etc.)
- **Terminal must be interactive** (not redirected or piped)

### Installation

1. Clone the repository:
```bash
git clone <your-repo-url>
cd MyFirstApp
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Build the project:
```bash
dotnet build
```

### Running the Application

```bash
dotnet run
```

Or run the compiled executable directly:
```bash
./bin/Debug/net8.0/MyFirstApp
```

### Expected Output

When you run the application, you'll see:

1. A centered ASCII art banner displaying "Hello, World!"
2. A blue, left-justified ASCII art text displaying "First GUI"
3. An interactive menu with multiple choices
4. Colored feedback based on your selection:
   - **Green**: Good person choices
   - **Red**: Bad person choices
   - **Yellow**: Neutral person choices

## Code Structure

### Program.cs

```csharp
using Spectre.Console;

class Program{
    static void Main(){
        // Display ASCII art banners
        AnsiConsole.Write(new FigletText("Hello, World!").Centered());
        AnsiConsole.Write(new FigletText("First GUI").LeftJustified().Color(Color.Blue));

        // Interactive selection prompt
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose your destiny")
                .PageSize(10)
                .AddChoices(new[]{...})
                .UseConverter(choice => choice.ToUpperInvariant())
        );
    
        // Switch statement for user feedback
        switch(choice){
            case "Be a good person":
                AnsiConsole.Write(new Markup("[green]You are a good person[/]"));
                break;
            // ... more cases
        }
    }
}
```

## Spectre.Console Key Concepts

### FigletText
Creates large ASCII art text from regular strings. Supports various fonts and styles.

```csharp
new FigletText("Text")
    .Centered()           // Center alignment
    .LeftJustified()      // Left alignment
    .Color(Color.Blue)    // Text color
```

### SelectionPrompt
Creates interactive menus where users can navigate with arrow keys and select with Enter.

```csharp
var choice = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("Menu Title")
        .PageSize(10)
        .AddChoices(choices)
);
```

### Markup
Allows inline styling of text using a simple markup syntax.

```csharp
AnsiConsole.Write(new Markup("[green]Success![/]"));
AnsiConsole.Write(new Markup("[red]Error![/]"));
AnsiConsole.Write(new Markup("[yellow]Warning![/]"));
```

## Project Configuration

### MyFirstApp.csproj

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Spectre.Console" Version="0.53.0" />
  </ItemGroup>
</Project>
```

## Building for Distribution

### Debug Build
```bash
dotnet build
```

### Release Build
```bash
dotnet build -c Release
```

### Self-Contained Deployment (macOS)
```bash
dotnet publish -c Release -r osx-x64 --self-contained
```

### Self-Contained Deployment (Windows)
```bash
dotnet publish -c Release -r win-x64 --self-contained
```

### Self-Contained Deployment (Linux)
```bash
dotnet publish -c Release -r linux-x64 --self-contained
```

The output will be in `bin/Release/net8.0/<runtime>/publish/`

## Troubleshooting

### "Terminal is not interactive" Error
This error occurs when the terminal doesn't support interactive input (e.g., when output is redirected).

**Solution**: Run the application directly in a terminal, not through pipes or redirects:
```bash
# ✅ Good
dotnet run

# ❌ Bad
echo "" | dotnet run
dotnet run > output.txt
```

### Colors Not Displaying
If colors aren't showing, ensure your terminal supports ANSI escape codes. Modern terminals like:
- macOS Terminal
- iTerm2
- Windows Terminal
- Visual Studio Code integrated terminal

all support colors by default.

### Build Errors After Git Pull
If you encounter build errors after pulling changes:

```bash
# Clean and rebuild
dotnet clean
rm -rf bin obj
dotnet build
```

## Extending the Application

### Adding More Menu Options

```csharp
var choice = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("Choose your destiny")
        .AddChoices(new[]{
            "Option 1",
            "Option 2",
            "New Option Here"  // Add new options
        })
);
```

### Adding More Visual Elements

Spectre.Console supports many other components:
- **Tables**: Display data in tabular format
- **Progress Bars**: Show progress for long-running operations
- **Trees**: Display hierarchical data
- **Panels**: Add bordered sections
- **Charts**: Display bar charts

Example:
```csharp
var table = new Table();
table.AddColumn("Name");
table.AddColumn("Value");
table.AddRow("Setting 1", "Enabled");
AnsiConsole.Write(table);
```

## Resources

- [Spectre.Console Documentation](https://spectreconsole.net/)
- [Spectre.Console GitHub](https://github.com/spectreconsole/spectre.console)
- [.NET 8.0 Documentation](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8)

## License

This is an educational project demonstrating Spectre.Console capabilities.

---

**Author**: Learning project for terminal UI development in .NET  
**Framework**: .NET 8.0  
**UI Library**: Spectre.Console 0.53.0  
**Platform**: Cross-platform (Windows, macOS, Linux)
