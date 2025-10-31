# MyFirstApp - Custom Console I/O Library

A .NET 8.0 console application demonstrating low-level system programming through Platform Invoke (P/Invoke) by implementing custom console I/O operations using direct POSIX system calls.

## Project Overview

This project consists of two components:

1. **MyConsoleLibrary** - A reusable class library providing custom console I/O functions
2. **MyFirstApp** - A demonstration console application utilizing the library

The project showcases how to bypass the standard .NET `Console` class and interact directly with the operating system's file descriptors using P/Invoke to call native `libc` functions.

## Architecture

### Project Structure

```
MyFirstApp/
├── MyFirstApp.csproj              # Main console application project
├── Program.cs                     # Application entry point
├── MyConsoleLibrary/              # Reusable class library
│   ├── MyConsoleLibrary.csproj    # Library project file
│   └── ConsoleWriter.cs           # Custom I/O implementation
└── README.md
```

### MyConsoleLibrary - Core Implementation

The `ConsoleWriter` class provides three static methods for console output:

#### Methods

**`MyWrite(string text)`**
- Writes text directly to stdout (file descriptor 1) without adding a newline
- Uses UTF-8 encoding for cross-platform text compatibility
- Throws `InvalidOperationException` if the write operation fails
- Useful for inline output or building output progressively

**`MyWriteLine(string text)`**
- Writes text to stdout with automatic newline handling
- Prepends and appends a newline character to ensure output starts on a fresh line
- Ideal for complete message output that should always appear on its own line
- Internally calls `MyWrite()` with `"\n" + text + "\n"`

**`MyWriteLineError(string text)`**
- Writes text directly to stderr (file descriptor 2) with a trailing newline
- Used for error messages and diagnostic output
- Separates error output from standard output stream

#### Technical Implementation

```csharp
[DllImport("libc", SetLastError = true)]
private static extern int write(int fd, byte[] buf, int count);

private const int STDOUT_FILENO = 1;
private const int STDERR_FILENO = 2;
```

The library uses P/Invoke to call the POSIX `write()` system call directly:
- **File Descriptors**: Uses standard UNIX file descriptors (1 for stdout, 2 for stderr)
- **Encoding**: Converts .NET strings to UTF-8 byte arrays before writing
- **Error Handling**: Checks return values and throws exceptions on failure
- **Platform**: Requires POSIX-compliant systems (macOS, Linux)

## Getting Started

### Prerequisites

- **.NET 8.0 SDK** or later
- **macOS or Linux** (POSIX-compliant system with `libc`)
- **Visual Studio Code** or any .NET-compatible IDE (optional)

### Building the Project

```bash
# Clone or navigate to the project directory
cd MyFirstApp

# Restore dependencies and build
dotnet build

# Build in Release mode
dotnet build -c Release
```

### Running the Application

```bash
# Run directly
dotnet run

# Or execute the compiled binary
./bin/Debug/net8.0/MyFirstApp
```

### Expected Output

```
Writing to stdout
No newline
But this is a newline
This is the standard console output
Writing to stderr
This is the standard console error
```

Note: stderr output may appear in a different order depending on your terminal's buffer handling.

## Usage Examples

### Basic Usage

```csharp
using MyConsoleLibrary;

// Write without newline
ConsoleWriter.MyWrite("Processing");
ConsoleWriter.MyWrite("...");
ConsoleWriter.MyWrite("Done!");

// Write with automatic newline
ConsoleWriter.MyWriteLine("Task completed successfully");

// Write errors to stderr
ConsoleWriter.MyWriteLineError("Warning: Configuration not found");
```

### Comparing with Standard Console

```csharp
// Standard .NET Console
Console.Write("Hello");          // High-level, buffered
Console.WriteLine("World");      

// MyConsoleLibrary - Direct system calls
ConsoleWriter.MyWrite("Hello");  // Low-level, direct to file descriptor
ConsoleWriter.MyWriteLine("World");
```

## Project Configuration

### MyFirstApp.csproj

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <ItemGroup>
    <ProjectReference Include="MyConsoleLibrary\MyConsoleLibrary.csproj" />
  </ItemGroup>

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <!-- Prevent duplicate type conflicts -->
  <ItemGroup>
    <Compile Remove="MyConsoleLibrary/**/*.cs" />
  </ItemGroup>
</Project>
```

The `Compile Remove` directive ensures that source files in the `MyConsoleLibrary` subdirectory are not compiled directly into the main project, preventing type conflicts since they're already referenced as a project dependency.

## Key Concepts Demonstrated

### 1. Platform Invoke (P/Invoke)
Direct interop with native C libraries from managed .NET code using `[DllImport]` attributes.

### 2. POSIX File Descriptors
Working with standard UNIX file descriptors:
- `0` - stdin (not used in this project)
- `1` - stdout (standard output)
- `2` - stderr (standard error)

### 3. Class Library Design
Separation of concerns by extracting reusable functionality into a dedicated library project.

### 4. Static Utility Classes
Using static classes for stateless utility functions that don't require instantiation.

### 5. UTF-8 Text Encoding
Proper text encoding when interfacing between .NET strings and byte-level system calls.

## Limitations & Considerations

### Platform Compatibility
- **Supported**: macOS, Linux, and other POSIX-compliant systems
- **Not Supported**: Windows (requires different P/Invoke declarations for `kernel32.dll`)

To add Windows support, implement conditional compilation:
```csharp
#if WINDOWS
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool WriteFile(IntPtr hFile, byte[] lpBuffer, 
        uint nNumberOfBytesToWrite, out uint lpNumberOfBytesWritten, IntPtr lpOverlapped);
#else
    [DllImport("libc", SetLastError = true)]
    private static extern int write(int fd, byte[] buf, int count);
#endif
```

### Performance Characteristics
- **Direct system calls** bypass .NET's buffering mechanism
- May be slower for small, frequent writes compared to buffered I/O
- Useful for understanding low-level I/O, but `Console.Write` is generally preferred for production code

### Error Handling
- `MyWrite()` throws exceptions on write failures
- `MyWriteLineError()` currently does not check return values (intentional for this demo)
- In production code, all system calls should have proper error handling

## Development Notes

### Why This Approach?

This project is primarily educational, demonstrating:
- How .NET interoperates with native code
- What happens "under the hood" when you call `Console.WriteLine()`
- The difference between managed and unmanaged code boundaries

For production applications, prefer using the standard `Console` class unless you have specific requirements for direct file descriptor manipulation.

### Extending the Library

To add more functionality:

```csharp
// Reading from stdin
[DllImport("libc", SetLastError = true)]
private static extern int read(int fd, byte[] buf, int count);

public static string MyReadLine()
{
    byte[] buffer = new byte[1024];
    int bytesRead = read(0, buffer, buffer.Length);
    return Encoding.UTF8.GetString(buffer, 0, bytesRead);
}
```

## Building for Distribution

### Self-Contained Deployment (macOS)
```bash
dotnet publish -c Release -r osx-x64 --self-contained
```

### Framework-Dependent Deployment
```bash
dotnet publish -c Release
```

The output will be in `bin/Release/net8.0/publish/`

## Troubleshooting

### Build Errors
```bash
# Clean and rebuild
dotnet clean
dotnet build
```

### Type Conflict Warnings
Ensure the `<Compile Remove>` directive is present in `MyFirstApp.csproj` to prevent the build system from compiling library source files twice.

### Runtime Errors on Write
- Verify you're running on a POSIX-compliant system
- Check that `libc` is available (standard on macOS/Linux)
- Ensure proper permissions for console output

## Contributing

This is a learning project, but improvements are welcome:
- Add Windows support via conditional compilation
- Implement input methods (reading from stdin)
- Add buffering for performance optimization
- Create unit tests using mocking frameworks

## License

This is an educational project. Feel free to use and modify as needed.

---

**Author**: Built as a demonstration of P/Invoke and low-level system programming in .NET  
**Target Framework**: .NET 8.0  
**Platform**: POSIX-compliant systems (macOS, Linux)
