# Error Handling 

```python
import os
import sys
import traceback
import warnings

# write errors to stderr
if some_error:
	sys.stderr.write("som error message")
	print("another way of writing error", file=sys.stderr)
	exit(1)

# write warnings
warnings.warn('bad bad bad')

# To catch an exception
try :
	5 // 0
except Exception as err : # Exception is the base of all error type, as err stores the error message in variable 'err'
	print(err)

# To Catch Specific exception
try :
	5 // 0 
except ZeroDivisionError as err : # Catch a specific type of error
	print('Did you try to divide by 0?')

# Raise your own error
try :
	if (os.path.exists('C:\\SomeFakeFile.txt')) :
		print('found you file')
	else :
		raise FileNotFoundError('Wheres that file?') # raise is like throw
except FileNotFoundError as fnfe:
	print("where dat file at? its certainly not at {0}".format(fnfe.filename))
except Exception as e :
	print(e)
	sys.exit(e.args[0]) # exit with correct return code
finally: 
	pass # do some other stuff

# Assert 
# You should not handle asserts with try try,except. If an assert fails you should fail the program
assert all([0,1,2,3]) # would raise becuase 0 is False

class MyError(Exception):
	pass
```