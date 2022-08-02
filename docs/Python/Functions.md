# Functions

```python
# basic syntax
def func1(x,y) :
	print("Inputs were {0} & {1}".format(x,y))

# example of mixing positional and named params and default values
def func2(x, y, *, z=''): 
	# anything after the * is treated as a named param only
	print()

# PACKING multiple inputs (not unpacking)
# in this case the SEPERATE args 1, 2 and 3 are packed up into a tuple
def func3(*args):
	print(args)

func3(1,2,3)

# PACKING of key-value args (not unpacking)
# in this case the three names params given are packed into a dict
def func4(**kwargs):
	print(kwargs)

func4(col='red', width=20, height=20)

# UNPACKING of args into a function call
def func5(a, b, c):
	print(c, b, a)

args = (1, 2, 3)
func5(*args)


# UNPACKING of key-value args into a function call
def func6(width, height, col):
	print(width, height, col)

kwargs = {'col':'red', 'width':20, 'height':30}
func6(**kwargs) # this unpacks the dict into seperat artgs
```


## Scope

```python
# functions are not scoped like in powershell
def multi(x):
	x *= 4

int1 = 4 # int is immutable so will not ber changed outsie of the func
multi(int1)
print(int1)

lis1 = [3, 2] # list is mutabel so the object in memory is altered resulting in the object outside the function changing
multi(lis1)
print(lis1)

# withing a function you can choose which scope variabel you want to trget 
global my_var # target the global my_var
nonlocal my_var # target the my_var in above scope (i.e. one level up)
```


## Lambda

```python
# annonomous function object

#sort
fruit = ['apple','bannana','orange']
fruit.sort(key=lambda x: x[1:]) #can be used to do complicated sorts

def all_but_first (x): # this is the equiv.
	return x[1:]

# map
list1 = [1,2,3,4,5,6,7,8]
# lambda will apply modulo 2 to each element
# map applies lambda to list 1 which returns a map obect
# list ocnvert bac to list for us
# the result is we have applied a function to every element in list 
list(map(lambda x: x%2, list1))

new_list = [] # equiv to above
for i in list1:
	new_list.append(i % 2)
list1 = new_list

# filter
items = \
  ['elephant', 'telescope', 'plinth', 
   'mouse', 'tripod', 'aardvark' ]

result1 = list(filter(lambda i: 'e' in i, items))
print(result1)

result2 = []   # equiv to above
for i in items:
    if 'e' in i:
        result2.append(i)
print(result2)

result3 = [ i for i in items if 'e' in i ] # equiv to above, this is more pythonic
print(result3)
```


# Generator

```python
# these are much more efficient becuase when returnd the list isnt created which may take up lots of memory
# you can iterate over this or just use list() to build a list when its needed. 
def frange(start, stop, step=.25):
    curr = float(start)
    while curr < stop and step > 0:
        yield curr
        curr += step

vals = [
    (1.1, 3),
    (1, 3, 0.33),
    (1, 3, 1),
    (3, 1),
    (1, 3, 0),
    (-1, -0.5, 0.1)
]

ranges = list(map(lambda x: list(frange(*x)), vals)) # funky way of calling same function with many different args
for r in ranges:
    print(r)

```