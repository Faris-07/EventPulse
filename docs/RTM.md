# Requirement Traceability Matrix (RTM) - EventPulse

| Requirement ID | Feature / User Story | Technical Component | Test Case ID | Test Layer | Status |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **REQ-TICK-01** | Max 4 tickets limit | `TicketOrderService.cs` | `UT-TICK-01` | Unit (xUnit) | Planned |
| **REQ-TICK-02** | Available stock check | `TicketOrderService.cs` | `UT-TICK-02` | Unit (xUnit) | Planned |
| **REQ-TICK-03** | 10-Min hold expiration | `POST /api/orders/hold` | `IT-TICK-01`<br>`PM-TICK-01` | API Integration<br>Postman Collection | Planned |
| **REQ-TICK-04** | Ticket selection UI | `Catalog.razor` | `E2E-TICK-01` | UI (Playwright) | Planned |
| **REQ-CPN-01** | 10% promo math | `DiscountService.cs` | `UT-CPN-01` | Unit (xUnit) | Planned |
| **REQ-CPN-02** | Min £50 spend validation | `POST /api/orders/apply-promo` | `PM-CPN-01` | Postman Collection | Planned |
| **REQ-CHK-01** | Order state transition | `CheckoutService.cs` | `UT-CHK-01` | Unit (xUnit) | Planned |
| **REQ-CHK-02** | 8-char ref code generator | `CheckoutService.cs` | `UT-CHK-02` | Unit (xUnit) | Planned |
| **REQ-CHK-03** | Full E2E Checkout Journey | Full App Pipeline | `PM-CHK-01`<br>`E2E-CHK-01` | Postman Collection<br>UI (Playwright) | Planned |
