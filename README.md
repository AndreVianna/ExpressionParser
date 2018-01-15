# C# Expression Parser

The project provides a simple expression parser that transforms a string into a valid C# expression representing a function.
The function can be called with or without a parameter or as part of a linq query.

## Getting Started

The library contains a static class with 4 methods:

```
Func<TOutput> ExpressionParser.Parse<TOutput>(string input)
Delegate ExpressionParser.Parse(string input)
Func<TInput, TOutput> ExpressionParser.ParseFor<TInput, TOutput>(string input)
Delegate ExpressionParser.ParseFor<TInput>(string input)
```

### Prerequisites

There is no prerequisite to install and use the methods included in this library.

### Installing

You can install the ExpressionParser by downloading it as a NuGet package:

```
Install-Package CSharp.ExpressionParser
```

After that you can just use the call directly from your code.
Here is a couple of usage example:

```
var result1 = ExpressionParser.Parse<int>("(3 + 2) * 3")(); //result1 should be an integer of value 15
var result2 = ExpressionParser.ParseFor<SomeClass, bool>("Id == 23 && IsActive == true")(instance);  //result2 should be a boolean that the value shoul depend on the instance provided as input
```

If you don't know the output of the result in advande you can use:
```
var expression = ExpressionParser.Parse(someStringToBeParsed); //expression will have a delegate returning an object.
var result = expression.DynamicInvoke();  //result will have the result of the expression as an object.
```
Here are a few samples of expressions that will be accepted by the case above:
```
"2.0 / 5.0" ==> Expected result: 0.4 (decimal)
"3 + 2 * 3" ==> Expected result: 9 (int)
"1 >= 1" ==> Expected result: true (bool)
```

This is also supported when a input value is provided, but in this case the type of the input has to be informed.
Here is an example:

```
var expression = ExpressionParser.ParseFor<SomeClass>(someStringToBeParsed);
var result = expression.DynamicInvoke(instaceOfTypeSomeClass);
```

More examples can be found in the test project.

## The tests

A Nunit test project is provided in the solution.

## Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/your/project/tags). 

## Authors

* **Andre Vianna** - *Initial work* - [AndreVianna]https://github.com/AndreVianna)

See also the list of [contributors](https://github.com/AndreVianna/ExpressionParser/graphs/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

