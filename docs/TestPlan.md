# Software Test Strategy & Test Plan (STLC Phase 2)

**Project:** EventPulse (Digital Event Ticketing Engine)  
**Author:** Faris
**Version:** 1.0  
**Status:** In Progress

---

## 1. Executive Summary & Objectives

This document outlines the end-to-end Quality Assurance Strategy and Test Plan for the **EventPulse** mini-ticketing SaaS platform. The core objective is to ensure high application reliability, fault isolation, accurate discount math, and concurrency-safe inventory holds across all product workflows before releasing to production.

---

## 2. Scope of Testing

### In-Scope (Core Features)
- **Ticket Catalog & Inventory:** Quantity selection validation (Max 4 tickets), dynamic stock deduction, and 10-minute hold expiration logic.
- **Promo Code Engine:** Subtotal calculations, threshold enforcement (Min spend £50 for `EARLY10`), and invalid/expired promo code rejection.
- **Checkout & Order Processing:** Customer detail validation, 8-character uppercase alphanumeric confirmation code generation, and state transitions (`Pending` -> `Confirmed`).
- **Cross-Layer Verification:** Unit logic, HTTP API integration contracts, Postman collections, and Playwright UI E2E journeys.

### Out-of-Scope (Future Iterations)
- Third-party payment gateway integration (Stripe/PayPal simulated via test stubs).
- Multi-currency / FX rate conversions.
- Load testing beyond 500 concurrent users.

---

## 3. Test Strategy & Testing Pyramid

I employ a **Shift-Left, Four-Tier Testing Pyramid** strategy to maximise feedback speed while keeping maintenance costs low:
| Layer | Framework / Tool | Execution Frequency | Primary Focus | Target Coverage |
| :--- | :--- | :--- | :--- | :--- |
| **Layer 1: Unit** | C#, xUnit, FluentAssertions, Moq | Every Git commit & PR | Domain business rules, promo calculations, validation helpers | $\ge 90\%$ Code Coverage |
| **Layer 2: API Integration** | C#, `WebApplicationFactory` | Every Git commit & PR | Controller endpoints, JSON schemas, HTTP status codes (`200`, `201`, `400`) | 100% Core Endpoints |
| **Layer 3: Network API** | Postman, Newman CLI | Nightly & CI Pipeline | Live HTTP calls, chained variables (`{{holdId}}`), headers, environment configurations | 100% API Workflows |
| **Layer 4: UI E2E** | TypeScript/C#, Playwright | Pull Request to `main` | Headless Chromium cross-browser flows, DOM element state, user journeys | Core Happy Paths |

---

## 4. Test Environment & Tooling Stack

- **Runtime Environment:** .NET 8.0 SDK / C#
- **IDE & Development:** Visual Studio Code
- **API Client:** Postman Desktop & Newman CLI (`npx newman`)
- **Browser Driver:** Playwright (Chromium Headless)
- **Continuous Integration:** GitHub Actions (`windows-latest`)
- **Test Management:** GitHub Issues, GitHub Projects (Kanban), and Markdown RTM

---

## 5. Risk Assessment & Mitigation Matrix

| Risk Scenario | Risk Level | Impact | Mitigation Strategy |
| :--- | :--- | :--- | :--- |
| **Race Conditions on Stock Hold** | High | Overselling event tickets | Enforce lock logic in `TicketOrderService` and write concurrent xUnit stress unit tests. |
| **Flaky E2E UI Tests** | Medium | False CI failures | Use Playwright web-first assertions and explicitly avoid hardcoded `Thread.Sleep()`. |
| **Postman / CI Runner Drift** | Low | Environment variable failures | Export environment files directly alongside collections in `tests/Postman/` and parameterise `baseUrl`. |

---

## 6. Exit Criteria & Quality Gates

The product will be formally approved for release when the following non-negotiable exit criteria are satisfied:

1. **Pass Rate:** 100% execution pass rate across all xUnit, Postman, and Playwright test suites.
2. **Coverage:** Minimum **90% branch coverage** verified on all backend C# services (`TicketOrderService`, `DiscountService`, `CheckoutService`).
3. **Pipeline Integrity:** GitHub Actions CI workflow executes cleanly on `windows-latest` without retries or ignored tests.
4. **Traceability:** All functional requirements in `docs/RTM.md` map to executed test IDs with status set to `Passed`.

