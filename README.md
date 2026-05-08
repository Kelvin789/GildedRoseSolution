# Gilded Rose – C# Solution

This solution is based on the Gilded Rose Requirements Specification and contains a C# implementation with xUnit unit tests.

## Project Structure

The solution contains two .NET 8 console projects:

- **GildedRose.Console**  
  Main inventory system application implementing the Gilded Rose business rules.

- **GildedRose.Tests**  
  xUnit test project containing unit tests covering item behaviours and edge cases.

## Requirements

Before running the solution, ensure the following is installed:

- Visual Studio 2022 or later (recommended)
- .NET 8.0 SDK

## Future Roadmap

- Improve error handling and logging so that both exception details and custom messages are recorded. This would make it easier to trace each step and identify where issues occur.
  - Unit tests for logging could also be improved by writing to a temporary test log file, asserting that it was created correctly, and then deleting it after the test.
  - In a production system, a logging framework such as Serilog, NLog, or Microsoft.Extensions.Logging could be used instead of manual text-file logging.
- Add further early-exit checks where appropriate to reduce unnecessary processing. For example, `EnsureQualityWithinLimits(Item item)` is currently called at the end of `UpdateQuality()`, but if an item’s quality is already at `0` or `40`, some update logic could be skipped depending on the item type and rule.
- Introduce a database or configuration store instead of hardcoding all values. This could include:
  - `ErrorLog` table for application errors.
  - `Log` table for traceability of key processing steps.
  - `Products` table for inventory item details.
  - `QualityLimits` lookup table for configurable minimum and maximum quality values, allowing changes without a code release.
