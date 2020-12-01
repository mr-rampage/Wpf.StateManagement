# Wpf.StateManagement
[![Build Status](https://travis-ci.com/mr-rampage/Wpf.StateManagement.svg?branch=master)](https://travis-ci.com/mr-rampage/Wpf.StateManagement)
[![Nuget](https://img.shields.io/nuget/v/Wpf.StateManagement)](https://www.nuget.org/packages/Wpf.StateManagement/)

The State Management Protocol is a protocol used for state management using events. This protocol was presented at the talk below and ported to WPF by leveraging routed events. This implementation was mainly built to facilitate the use of the Flux pattern in WPF instead of the MVVM pattern. This implementation only provides the mechanisms to write to the store and should be used in conjunction with the [Wpf.DependencyResolution](//github.com/mr-rampage/Wpf.DependencyResolution) protocol to expose the store to descendant elements.

[![Building a Complex Application with Web Components and LitElement](https://img.youtube.com/vi/x9YDQUJx2uw/0.jpg)](http://www.youtube.com/watch?v=x9YDQUJx2uw)

This implementation provides extensions that can be used with elements to allow elements to store state and decouple side effects. When using this protocol, an implementation of the store must be provide through the `IStore` interface. This allows for extremely basic store implementation, as well as more complex implementation using observables, such as [ReactiveX](http://reactivex.io/).

The use of this protocol assumes knowledge of Flux and/or any other implementations such as Elm and Redux.

## Usage

1. Implement a store using the `IStore` interface.
2. Create routed events by extending the `FluxAction` class to request updates to the store.
3. Create reducers using the `IReducer` interface to update the store.
4. Call `ProvideStore` using the `StoreProvider` extension on the element to act as the orchestrator.
5. (Optional) Using [Wpf.DependencyResolution](//github.com/mr-rampage/Wpf.DependencyResolution), expose the store to descendants using the `ProvideInstance` method from the `InstanceProvider` extension.
6. (Optional) Call `Enhance` using the `Enhancer` extension on any descendant element to intercept, modify, or make side effecting calls. Enhancers are applied starting from the Enhancer farthest away from the StoreProvider in the element tree. As well, create any other routed events by extending `FluxAction` that may be required by the Enhancer.
7. Call `Dispatch` with routed events extending `FluxAction`.

## API

### IStore interface

#### State property

This getter provides the current state of the store.
#### Dispatch

This method should trigger the generation of new state. It should consequently trigger a property change on the State property.

### IReducer interface

#### Reduce

This method is the implementation of the Reducer to provide the new state of the store.

### FluxAction class

This class is a wrapper for RoutedEventArgs and is mainly used to identify routed events meant for State Management.

### StoreProvider

#### ProvideStore

By calling `ProvideStore` with a `IStore` within an element, that element becomes a listener for any `FluxActions` and will dispatch those actions to the `IStore`. This is our bridge between the WPF event system and the store.

### Enhancer

#### Enhance (AKA Middleware)

By calling `Enhance` with a `DispatchHandler` within an element, that element becomes a listener for any `FluxActions` and will execute the `DispatchHandler`. This is useful to executing side effecting code. In an ideal setup, all side effecting code should be isolated within services that collaborate with the `Enhancer`. This allows the `Enhancer` to be tested easily as a series of dispatches. Once again, [Wpf.DependencyResolution](//github.com/mr-rampage/Wpf.DependencyResolution) can be used to inject services into the `Enhancer`.

### Dispatcher

#### Dispatch

By calling `Dispatch` with any `FluxAction` within an element will trigger a store update at the `StoreProvider`. Any `Enhancer` in between the element and the `StoreProvider` can react to the dispatched `FluxAction`. `Dispatch` is basically a wrapper for the native `RaiseEvent`, but can be used to replace the event bus within the protocol if required.
