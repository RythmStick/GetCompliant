# GetCompliant
## Extracts passwords from file that match specified complexity.

For example, you want to run a dictionary attack using rockyou.txt, against a Windows domain that enforces password complexity.
But you know that >90% of those rockyou.txt passwords are invalid against your target.

### Usage:
 
    -i, --inputfile=VALUE      Password File eg. rockyou.txt
    -o, --outputfile=VALUE     Output File
    -f, --forceoverwrite       force overwrite if Output file exists
    -m, --minlength=VALUE      Minimum password Length
    -x, --maxlength=VALUE      Maximum password Length
    -u, --upper                must include Uppercase set
    -l, --lower                must include Lowercase set
    -p, --special              must include Special Char set
    -n, --number               must include Number set
    -s, --sets=VALUE           number of Mandatory sets (default: All Sets)
    -h, -?, --help             Show Help
    
For details see https://www.rythmstick.net/posts/getcompliant

