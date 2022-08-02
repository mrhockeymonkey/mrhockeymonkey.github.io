# Modules

```python
import sys 

"""
This is the description for the module
"""
def func():
    """
    This is a doc string for function
    """
help(sys) # retreive descriptions and list of functions
help(func) # retreive doc strings for module

# Convention to run as a script but also be importable as a module safely
def main():
    pass # do something

if __name__ == '__main__':
    main()

```