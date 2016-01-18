## CRM Unit Testing Extensions (CUTE)

Despite there are several unit tesing libraries on the market that deal with MS Dynamics CRM, none of them fully comply with MS CRM developer needs. Without any ambition of creating something either universal or unique we combined several common approaches used into one small library that let you finally kick start unit testing in your Dynamics CRM project.

### Currently `CUTE v.1` has following features:

* Capture CRM requests and responses during plugin execution, and reuse this information during test run;
* Proxy all request to existing CRM instance and return real answers to plugin during test run;
* Manually composing `IPluginExecutionContext` without need of mocking library;
* Ability to simmulate isolation mode, i.e. testing plugin execution in sandboxed environment (CRM Online);
* Ability to record and throw `IOrganizationService` exceptions, so most of plugin usage cases are covered;