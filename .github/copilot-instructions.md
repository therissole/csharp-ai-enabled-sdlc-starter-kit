# Repository-Wide Copilot Instructions

## Project Overview
This is a C# AI-enabled SDLC starter kit designed to accelerate development using agentic AI. The repository serves as a foundation for C# applications with best practices for AI-assisted development.

## Technology Stack
- **Language**: C# (.NET)
- **Target Framework**: .NET 8.0 (or latest LTS)
- **Build System**: MSBuild / dotnet CLI
- **Testing Framework**: xUnit (recommended) or NUnit
- **Package Manager**: NuGet

## Development Guidelines

### Code Style and Conventions
- Follow C# coding conventions as outlined in Microsoft's official guidelines
- Use PascalCase for class names, method names, and public members
- Use camelCase for local variables and private fields (with underscore prefix `_` for private fields)
- Use meaningful, descriptive names for all identifiers
- Add XML documentation comments for all public APIs
- Keep methods focused and small (single responsibility principle)
- Use `async`/`await` for asynchronous operations
- Prefer `var` when the type is obvious from the right side

### Code Quality
- Write unit tests for all new functionality
- Maintain test coverage above 80%
- Ensure all tests pass before committing
- Use dependency injection where appropriate
- Follow SOLID principles
- Handle exceptions appropriately with meaningful error messages
- Avoid using `null` when possible; prefer nullable reference types

### Project Structure
- Place source code in `src/` directory
- Place tests in `tests/` or `test/` directory
- Use feature-based or layer-based folder organization
- Keep configuration files in the root directory

### Build and Test Commands
```bash
# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run tests
dotnet test

# Run with specific configuration
dotnet build --configuration Release
dotnet test --configuration Release
```

### Security Guidelines
- Never commit secrets, API keys, or passwords
- Use environment variables or secure key vaults for sensitive data
- Regularly update dependencies to patch security vulnerabilities
- Validate and sanitize all user inputs
- Use parameterized queries to prevent SQL injection

### Git Workflow
- Create feature branches from `main`
- Use descriptive commit messages
- Keep commits atomic and focused
- Squash commits before merging when appropriate

## AI Agent Boundaries
- Never modify `.git/` directory or git configuration
- Never commit sensitive information (secrets, keys, passwords)
- Always run tests after making code changes
- Preserve existing functionality unless explicitly asked to change it
- Follow the minimal change principle - make the smallest possible changes to achieve the goal

## Documentation
- Update README.md when adding significant features
- Maintain inline code comments for complex logic
- Keep XML documentation comments up to date
- Document breaking changes in commit messages
