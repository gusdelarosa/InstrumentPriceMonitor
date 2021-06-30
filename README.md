# Instrument Price Monitor

WPF application that displays simulated instrument prices


## Assumptions

- Any ticker the UI subscribes to exist and will generate data
- Prices up to two decimal places
- Any ticker entered is valid, no validation exists
- Prices displayed are simulated, not real prices

## Overview

- The Engine 
  - Dependency: Collection of Price Sources
  - The engine subscribes to published data from all of its price sources 
  - When new instrument  Instrument Data is published by any of the price sources, the engine then relays that information to its subscribers
- Ticker repo
  - In an app where the price sources are not simulated, we do not need this. It is something that our app needs to simulate/support ANY ticker that the UI subscribes to
  - In a production environment, each price source will have all of their supported intruments
