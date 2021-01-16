<#
    .SYNOPSIS
    Playing with emojis on Powershell 7 + Windows Terminal

    .NOTES
    Unicode & ASCII:
    Both are "standards" for encoding and representing text. ASCII is older and small using 8 bits whilst
    unicode uses up to 32 bits and is widely adopted. Unicode also has codepoints for emojis which is why we need to know this

    The below are all "implementations" of unicode
    UTF-8: uses variable size (1-4 bytes) to encode. Since ASCII is 1 byte anything ASCII test is automatically UTF-8 also
    UTF-16: uses exactly 2 bytes and so is unable to express some characters
    UTF-32: uses exactly 4 bytes and so is much larger than UTF-8 and not widely used
#>

# to use emojis you need only know the codepoint for it which is expressed as U+ and some hex value. (see: https://unicode-table.com/en/sets/faces/)
# 🔥 == U+1F525 
# 💩 == U+1F4A9

# OPTION 1 - the official and easy way to display a unicode character is
"`u{1F4A9}"

# OPTION 2 - another way is to use [char]
[char]::ConvertFromUtf32(0x1F4A9)

# technically ConvertFromUtf32() takes an int32 as an argument. this is where powershell has automatically converted hex to int32 for us
$emojiInt = [int]0x1F4A9 # 128169
[char]::ConvertFromUtf32($emojiInt)

<#
    .NOTES
    seeing utf32 above confused me initially since powershell uses UTF-8.
    But remeber UTF-8 is "variable" so in this we are converting from 0x1F4A9 which is 3 bytes.
    This also explains why we dont see a ConvertFromUtf8() method becuase it would needlessly limit us 
#>

# when used a type accelerator [char] can display a character encoded as a single byte. aka ASCII or UTF-8 (but not all of it, againm its variable)
[char]0x21 # hex for '!'
[char]33 # byte for '!'
[char]"!" # string for '!'
[char]0x21 -eq [char]33 -eq "!" # True

# OPTION 3 - for completeness
[System.Text.Encoding]::UTF32.GetString([System.BitConverter]::GetBytes(0x1F4A9))