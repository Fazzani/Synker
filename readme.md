# Synker API

## Architecture (Clean architecture)

Inspired from [NorthwindTraders][NorthwindTraders]
[Slides](https://github.com/Fazzani/Synker/raw/master/docs/Slides.pdf)

### Schema

![archi_clean_diagram](https://fullstackmark.com/img/posts/11/clean-architecture-circle-diagram.jpg)

### Micro-services set

- Playlists service
  - CRUD
  - Synchro (by playlist && by group)
  - Decorators
    - Cleaning names
    - Auto grouping
    - Shift time
    - Custom && dynamic
- EPG service
- Picons service
- Authentication Service (IDP)
- Notification Service

## Techno and frameworks

- OPEN API 3 with [NSwag][nswag_repo]
- Event sourcing
- [MediatR](https://github.com/jbogard/MediatR/wiki)
- TDD:
  - Xunit, 
  - [anglesharp][anglesharp] 
  - [Moq][moq_repo]
  - e2e with [Cypress][cypress]
- Prometheus metrics
- [Beat Pulse][beat_pulse_github] for liveness and readiness check
  - BeatPulse.IdSvr
  - BeatPulse.Elasticsearch
  - BeatPulse.Npgsql
- Serilog for logging
- [AutoMapper](https://automapper.org/)
- [Fluentvalidation](https://fluentvalidation.net/start)
- [Command CQRS validation](https://www.linkedin.com/pulse/validation-ddd-cqrs-luca-briguglia/)
- [DDD Aggregate pattern](https://martinfowler.com/bliki/DDD_Aggregate.html)

## Clean Architecture

### Designing Domain Model Layer

1. **Persistence Ignorance (PI) principle** says that the Domain Model should be ignorant of how its data is saved or retrieved:
   - No data access code
   - No data annotations for our entities
   - No inheritance from any framework classes, entities should be Plain Old CLR Object
2. Putting all data access code outside our domain model implementation
3. Using Entity Framework Core features: 
   - Shadow Properties
   - Owned Entity Types
   - Private fields mapping
   - Value Conversions

## Go further & references

- [Clean Architecture][clean_archi]
- [Onion Architecture][onion_archi]
- [Ports And Adapters / Hexagonal Architecture][port_adapter_hexa]
- [Example Clean Archi 1][example_archi_1]
- [Example Clean Archi 2][example_archi_2]

[beat_pulse_github]:https://github.com/Xabaril/BeatPulse
[NorthwindTraders]:https://github.com/JasonGT/NorthwindTraders
[Persistence Ignorance]:http://www.kamilgrzybek.com/design/domain-model-encapsulation-and-pi-with-entity-framework-2-2/
[nswag_repo]:https://github.com/RicoSuter/NSwag
[moq_repo]:https://github.com/moq/moq4
[anglesharp]:https://anglesharp.github.io/
[clean_archi]:http://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html
[onion_archi]:https://jeffreypalermo.com/2008/07/the-onion-architecture-part-1
[port_adapter_hexa]:https://herbertograca.com/2017/09/14/ports-adapters-architecture/
[example_archi_1]:https://fullstackmark.com/post/11/better-software-design-with-clean-architecture
[example_archi_2]:https://fullstackmark.com/post/18/building-aspnet-core-web-apis-with-clean-architecture
[cypress]:https://www.cypress.io/