# Classes

```python
import random

wildpokemon = ['zubat','vulpix','staryu','sandshrew']

class Pokemon: # class names use CapWords
	population = 151 # static variable relating to the class itself not the instance
	
	def __init__(self, name, lvl) : # constructor
		self.name = name # setup the object variables
		self.lvl = lvl
		self.__hp_multiplier = 50 # __ convention for class private variables of methods
		self.__calculate_stats()

	def __calculate_stats(self) :
		self.hp = self.lvl * self.__hp_multiplier
		self.atttack = self.lvl * 2
		self.defence = self.lvl * 1.5

	def attack(self):
		pass

class WildPokemon(Pokemon):
	def __init__(self):
		self.name = wildpokemon[random.randint(0,3)]
		self.lvl = random.randint(5,15)
		super().__init__(self, self.name, self.lvl) # inherit, could also use Pokemon.__init__()

p = Pokemon()
print('a wild', p.name, 'has appeared!')
print('Level:',p.lvl)
```

## Inheritance

```python
import random

wildpokemon = ['zubat','vulpix','staryu','sandshrew']
ledgpokemon = ['articuno','zapdos','moltres','mewtwo']

class Pokemon(object) :
	def __init__(self,xHP,xAtk,xDef) :
		self.name = 'missingno'
		self.lvl = 0
		self.xHP = xHP
		self.xAtk = xAtk
		self.xDef = xDef
	def calcHP(self) :
		return self.lvl * self.xHP
	def calcAtk(self) :
		return self.lvl * self.xAtk
	def calcDef(self) :
		return self.lvl * self.xDef

class WildPokemon(Pokemon) :
	def __init__(self) :
		super(WildPokemon,self).__init__(50,2,1)
		self.name = wildpokemon[random.randint(0,3)]
		self.lvl = random.randint(5,15)

class LedgPokemon(Pokemon) :
	def __init__(self) :
		super(LedgPokemon,self).__init__(100,5,4)
		self.name = ledgpokemon[random.randint(0,3)]
		self.lvl = random.randint(75,90)

		

p = LedgPokemon()
print('a wild', p.name, 'has appeared!')
print('Level:',p.lvl)
print('HP:',p.calcHP())
print('Atk:',p.calcAtk())
print('Def:',p.calcDef())

```

## Special Methods

```python

"""
special methods
"""
class SpecialMethods():
	def __bool__(self):
		pass # dictate truthyness
	def __str__(self):
		pass # return human readable string representation
	def __repr__(self):
		pass # return string that could be used to create that instance. eg "Pokemon(x, y)"
	def __len__(self):
		pass # return a size for the class
	def __hash__(self):
		pass # return suitable dict or set
	def __add__(self, value):
		pass
	def __sub__(self, value):
		pass
	def __eq__(self, value):
		pass # or ge, lt

"""
decorater
"""
class Protected:
	def __init__(self):
		self.__size = 30 # these is protected
		self.__password = 1234
	
	@property # now we designate this a property users can mess with
	def size(self):
		return self.__size
	
	@size.setter # this allows users to update the property in a prescribed way
	def size(self, value):
		self.__size = value

	@property # becuase there is no setter we block users form updating the value
	def password(self):
		return self.__password

	@staticmethod
	def meth1(self):
		pass
	
	@classmethod
	def meth2(self):
		pass
```
