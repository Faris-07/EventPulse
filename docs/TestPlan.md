# Software Test Strategy & Test Plan (STLC Phase 2)

**Project:** EventPulse (Digital Event Ticketing Engine)  
**Author:** SDET / Lead Quality Engineer  
**Version:** 1.0  
**Status:** Approved  

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

I employ a **Shift-Left, Four-Tier Testing Pyramid** strategy to maximize feedback speed while keeping maintenance costs low:
