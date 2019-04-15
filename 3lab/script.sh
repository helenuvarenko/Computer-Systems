#!/bin/sh
ml icc
lscpu > p
f_list=`grep "Flags" ./p | uniq | cut -d':' -f2 | cut -d" " -f2- | tr _ . | tr "a-z" "A-Z"` 
	for f in $f_list
		do
		icc -O2 -x$f fibo.cpp -o check.out 2> err
			if [ ! -s "error" ]; then
				for o in {1..3}
				do
						echo "______________________________" 
						echo "$f -O$o compiled in"             
						time `icc -O$o -x$flag 3ch.cpp`
						echo "++++++++++++++++++++++++++++++"
						echo "program ran 100 times in"
						time `for i in {1..100}; do ./a.out >/dev/null; done`
				done
			fi
		done;