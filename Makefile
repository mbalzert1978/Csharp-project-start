# Constants
DOTNET := dotnet
BUILD_CONFIG := Release
FORMATTER := csharpier

# Default target
.DEFAULT_GOAL := help

.PHONY: build ci clean analyze format test update help

help:
	@echo "Available targets:"
	@echo ""
	@echo "	help	- Show this help message"
	@echo "	build	- Build the solution in Release mode"
	@echo "	clean	- Clean build artifacts"
	@echo "	analyze	- Run code analysis using Roslynator"
	@echo "	format	- Format code using CSharpier and dotnet format"
	@echo "	test	- Run all tests"
	@echo "	update	- Update .NET tools"
	@echo "	ci	- Run full CI pipeline (clean, build, analyze, format, test)"
	@echo ""

build:
	$(DOTNET) build -c $(BUILD_CONFIG)

clean:
	$(DOTNET) clean

analyze:
	$(DOTNET) tool restore
	$(DOTNET) roslynator analyze

format:
	$(DOTNET) $(FORMATTER) .
	$(DOTNET) format --verify-no-changes

test:
	$(DOTNET) test

update:
	$(DOTNET) tool restore
	$(DOTNET) outdated -u

ci: clean build analyze format test
	@echo "CI pipeline completed successfully"