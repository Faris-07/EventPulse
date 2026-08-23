# EventPulse

A .NET event ticketing and checkout system, built as a hands-on portfolio project to practise professional software quality engineering — ticket-driven development, requirement traceability, and a full multi-layer test strategy.

## What it does

EventPulse handles the core flow of buying event tickets:

- Ticket catalog browsing with a max-per-order limit
- Real-time stock checks with a time-limited hold on selected tickets
- Promo code engine with minimum-spend validation
- Checkout with order state transitions and confirmation code generation

## Tech stack

- **Backend:** ASP.NET Core Web API (.NET 8, C#)
- **Frontend:** Blazor
- **Testing:** xUnit + FluentAssertions + Moq (unit), `WebApplicationFactory` (API integration), Postman/Newman CLI (network API), Playwright (E2E/UI)
- **CI/CD:** GitHub Actions (windows-latest)

## How this project is run

Every feature is tracked as a GitHub Issue and driven by three living documents:

- **[Test Plan](docs/TestPlan.md)** — the QA strategy: scope, a four-tier testing pyramid, environment/tooling, a risk assessment (e.g. race conditions on ticket stock holds), and formal exit criteria
- **[Definition of Done](docs/DefinitionOfDone.md)** — the quality gates every feature must pass before it's considered complete (coverage thresholds, CI status, PR-to-issue traceability)
- **[Requirement Traceability Matrix](docs/RTM.md)** — maps each requirement to its owning component and test case(s) across the unit, API, and E2E layers

## Status

Actively in development. Requirements and their corresponding tests are tracked in the RTM and executed against the Test Plan's exit criteria as features are built out.
