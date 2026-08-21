# Definition of Done (DoD) & Quality Gates - EventPulse

A User Story or Technical Task is considered **DONE** only when all criteria are met:

### 1. Code & Architecture Quality
- [ ] Feature code written according to clean architecture principles.
- [ ] No hardcoded secrets, connection strings, or API credentials.

### 2. Automated Test Coverage
- [ ] **Unit Tests:** All business logic covered with minimum **90% branch coverage**.
- [ ] **API Tests:** Endpoint returns correct HTTP Status Codes (`200`, `201`, `400`) and JSON schemas.
- [ ] **Postman Tests:** Collection assertions pass locally and via Newman CLI.
- [ ] **E2E Tests:** Critical paths automated with Playwright using `data-testid` selectors.

### 3. CI/CD & Pipeline
- [ ] GitHub Actions CI workflow executes and passes 100% cleanly.
- [ ] No flaky test retries or ignored tests without explicit ticket references.

### 4. Traceability
- [ ] Pull Request is linked to the corresponding GitHub Issue (`Fixes #1`).
- [ ] Requirement Traceability Matrix (`docs/RTM.md`) updated with new test IDs and status (`Passed`).
