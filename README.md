# MyLisp

## Valid functions in simulation
 1. add(a,b) -> a + b 
 2. print(a) -> prints a
 3. sub(a,b) -> a - b
 4. cmp(a,b) -> a == b
 5. div(a,b) -> a / b
 6. mul(a,b) -> a * b
 7. var(a) -> declares a with value 0, a - name of variable. (exmp var(var_name))
 8. if(a,b,c) -> a - condition, b - executes if a == true, c - executes if a != true
 9.  assign(a,b) -> a = b for variables declared by var(a)
 10. loop(a,b) -> a - condition, b - executes if a == true
 11. ls(a,b) ->  a < b
 12. gr(a,b) -> a > b
 13. mod(a,b) -> a % b
 14. pass() -> is similar for pass from python
## Valid functions in LLVM compilation
 1. add(a,b) -> a + b 
 2. print(a) -> prints a
 3. sub(a,b) -> a - b
 4. mul(a,b) -> a * b
 
 ## Usage for llvm
```
clang llvm_output.ll
```
## Examples of programs
```lua
print(add(15,add(15,15)))
print(add(16,add(15,15)))
print(add(16,add(15,add(10,5))))
print(sub(50,5))
print(mul(5,17))
```
```lua
var(a)
assign(a,34)
print(a)

assign(a,add(a,35))
print(a)

if(
    cmp(1,1),
    print(add(10,15)),
    mul(100,5)
)

print(add(100,100))
```
```lua
if(
    cmp(1,3),
    {
        print(1)
        print(2)
        print(3)
    },
    {
        print(4)
        print(5)
        print(6)
    }
)
```
```lua
var(iter)
loop(ls(iter,100),{
    if(cmp(mod(iter,2),0) ,print(iter),pass())
    assign(iter,add(iter,1))
})
```