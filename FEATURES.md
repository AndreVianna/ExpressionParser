### C# Expression Parser Supported Operators

Here is the list of the C# operators supported by the parser in the order of precedence.
Each section has a higher precedence than the next one.

#### Primary Operators  
  * `x.y` – Member Access;  
  * `x?.y` – Null condition member access;  
  * `f(x)` – Function invocation;  
  * `a[x]` – Aggregate object indexing;  

#### Unary Operators  
  * `+x` – Numeric value of `x`;  
  * `-x` – Numeric negation;  
  * `!x` – Logical negation;  
  * `(T)x` - Type cast.  

#### Multiplicative Operators  
  * `x * y` – Multiplication;  
  * `x / y` – Division (if the operands are integers the result is an integer truncated down);  
  * `x % y` – Modulus (the remainder of the integer division between `x` and `y`);  

#### Additive Operators  
  * `x + y` – Addition;  
  * `x - y` – Subtraction;  

#### Relational Operators  
  * `x < y` – Less than;  
  * `x > y` – Greater than;  
  * `x <= y` – Less than or equal to;  
  * `x >= y` – Greater than or equal to;  
  * `x is T` - Type compatibility.  
  * `x as T` - Type convertion.  

#### Equality Operators  
  * `x == y` – Equal to (by default, for reference types other than string, this returns reference equality);  
  * `x != y` – Not equal to (see comment for `==`);  

#### Conditional AND Operator  
  * `x && y` – Logical AND;  

#### Conditional OR Operator  
  * `x || y` – Logical OR;  

#### Null-Coalescing Operator  
  * `x ?? y` – Returns `x` if it is non-null, otherwise, returns `y`;  

#### Conditional Operator  
  * ~~`t ? x : y` – if test `t` is true, returns `x`, otherwise, returns `y`;~~ [Not Supported. Planned for future versions]

#### Lambda Operator  
  * `=>` – Lambda declaration;  


### C# "Out of the Box" Supported Types 
No need to call the Using method.

````
bool	        Boolean
byte	        Byte
sbyte	        SByte
char	        Char
decimal	        Decimal
double	        Double
float	        Single
int 	        Int32
uint	        UInt32
long	        Int64
ulong	        UInt64
object	        Object
short	        Int16
ushort	        UInt16
string	        String
````