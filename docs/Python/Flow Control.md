# Flow Control

```python
"""
if statement
"""
if 1 < 2 :
	print("this")
elif 2 > 3 :
	print("that")
else :
	print("those")

# boolean operators
a = 999
b = 999
a == b # True becuase values are the samer
a is b # False becuase they are different object (different ids) id(a)
a != b # False becuase they are the same, also can have <, <=, >, >=

bool(0) # False, also {}, [], (), None are False
bool(1) # True
all([1,2,3]) # True becuase ach element is true
all([0, 1, 2]) # False becuase 0 is false
any([0, 1]) # True becuase at least one item has a boolean true value

"""
while loop
"""
myl = [1, 2, 3, 4]
while myl:
	print(myl.pop(0) * 2) # pop(0) removes elemtns from list. could also be pop() which goes reverse

i = 1
j = 120
while i < 42:
	if i == 2:
		pass # emtpy placeholder
	if i == 3:
		continue # perform next iteration
	if i == 4:
		break # break out of the loop completely
else:
	print("loop expired")


"""
for loop
"""
for i in range(0, 10):
	print(i)

my_dict = {'hello': 1, 'world': 2}
for k,v in my_dict.items():
	print("Key is {0}, Value is {1}".format(k,v))

enumerate() # can be used to make things enumerable by returing (n, val) for each in collection


"""
with
"""
# when using with, a context manager, you dont need to close(). 
with open('fole.txt') as ifile:
	for line in ifile:
		print(line)

```