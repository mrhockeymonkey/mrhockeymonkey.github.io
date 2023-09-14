# Low Level Fun

## Prooving that &str points to the program itself

For a while I couldnt understand why `String::from("foo")` was a reference and how this worked  and also just assumed that meant it must be allocated on the heap but I didnt understand fully, so I had a poke around

```rs
fn main() {
        stringy();
}

fn stringy() {
        let a = String::from("foo");
        let b = String::from("bar");
        nothing()
}

fn nothing(){
        // this is just useful to see in assembly as a marker
        let a = 0;
}
```

Running this with a break point on `nothing()` we can start exploring in GDB (comments added)

```bash
disas
Dump of assembler code for function rust_basics::stringy:
# decrement the stack pointer for two 32bit refs and 8 bits for ... something ese im not sure
   0x000055555555d410 <+0>:	sub    $0x48,%rsp # 72 bits allocated on stack
# load effective addresss from 0x36c4e(%rip) which is a certain ofset from the instruction pointer, clever!
=> 0x000055555555d414 <+4>:	lea    0x36c4e(%rip),%rsi        # 0x555555594069
   0x000055555555d41b <+11>:	lea    0x8(%rsp),%rdi
   0x000055555555d420 <+16>:	mov    $0x3,%edx
   0x000055555555d425 <+21>:	mov    %rdx,(%rsp)
   0x000055555555d429 <+25>:	callq  0x55555555c780 <<alloc::string::String as core::convert::From<&str>>::from>
   0x000055555555d42e <+30>:	mov    (%rsp),%rdx
   0x000055555555d432 <+34>:	lea    0x36c33(%rip),%rsi        # 0x55555559406c
   0x000055555555d439 <+41>:	lea    0x20(%rsp),%rdi
   0x000055555555d43e <+46>:	callq  0x55555555c780 <<alloc::string::String as core::convert::From<&str>>::from>
   0x000055555555d443 <+51>:	jmp    0x55555555d461 <rust_basics::stringy+81>
   0x000055555555d445 <+53>:	lea    0x8(%rsp),%rdi
   0x000055555555d44a <+58>:	callq  0x55555555c870 <core::ptr::drop_in_place<alloc::string::String>>
   0x000055555555d44f <+63>:	jmp    0x55555555d4aa <rust_basics::stringy+154>
   0x000055555555d451 <+65>:	mov    %rax,%rcx
   0x000055555555d454 <+68>:	mov    %edx,%eax
   0x000055555555d456 <+70>:	mov    %rcx,0x38(%rsp)
   0x000055555555d45b <+75>:	mov    %eax,0x40(%rsp)
   0x000055555555d45f <+79>:	jmp    0x55555555d445 <rust_basics::stringy+53>
# this is our nothing fn so we can ignore everything else as runtime stuff
   0x000055555555d461 <+81>:	callq  0x55555555d4c0 <rust_basics::nothing>
<omitted>  
End of assembler dump.
```

So we see two `lea` instructions pointing at locations of memory that are within the actual program itself.

```bash
# examine 3 bytes from given address, this looks like it could be foo
x/3x 0x555555594069
0x555555594069:	0x66	0x6f	0x6f

# examine 3 chars from addr
x/3c 0x555555594069
0x555555594069:	102 'f'	111 'o'	111 'o'

interestingly foo and bar are right next to each other here...
I had expected something else like there to be some valid instructions in between so rust must be seperating instructions from string literals when compiling
x/6c 0x555555594069
0x555555594069:	102 'f'	111 'o'	111 'o'	98 'b'	97 'a'	114 'r'
```

TODO : So now I want to understand two more things. How data is stored in the output binary and what happens next

So now we have the data "foo" stored in a register...
