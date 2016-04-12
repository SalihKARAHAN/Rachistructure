# **Rachistructure**

Rachistructure provides basic attribute based *aspect oriented programming* infrastructure. I wrote two years ago (31.07.2014) but it had same bugs. Now, I fixed it. If you want to AOP in your application and you don't want to make complex configuration, you can use this.

## How to use?

  First, you must create new instance from `InstanceProvider` and map types with instance like following example:

```csharp

InstanceProvider instanceProvider = new InstanceProvider();

instanceProvider.Register<ITypeA, TypeA>();
instanceProvider.Register<ITypeB, TypeB>();
instanceProvider.Register<ITypeC, TypeC>();  

```

`InstanceProvider` not static object so if you use in other class as a static property, you access to easy.

```csharp
public class TypeContainer
{
  public static readonly InstanceProvider InstanceProvider = new InstanceProvider();  
}

...

public static Main(string[] args)
{
  ITypeB typeBInstance = TypeContainer.InstanceProvider.Resolve<ITypeB>();
}
```

For after than, you can do `Resolver<T>()` method for create instance by types. But I give support only constructor injection and you must a interface class for use this library.


```csharp
```
