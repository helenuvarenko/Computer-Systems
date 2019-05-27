%include 'defs.h'

	%macro printer 2
mov rax, SYS_WRITE
mov rdi, STDOUT
mov rsi, %1	;text to print - first arg
mov rdx, %2	;length of text - second arg
syscall
	%endmacro

section .data
bad_args_msg db "Must pass only 1 argument as filename to read", 10, 0
bad_msg_len equ $-bad_args_msg 	;calculated length of message
	
section .bss
	filename resq 1
	len_text resq 1
	pointer resq 1

section .text 
	global _start
_start:
	cmp qword [rsp], 2 	;check for only 2 arguments (program name and file name)
	jne bad_exit

f_open:	;finding name of needed file and opening it for reading
	mov rax, SYS_OPEN
	mov rdi, qword [rsp + 16] ;place of second arg on stack...
				  ;name of executable is the first arg and 
				  ;second arg is filename to read 
	mov rsi, O_RDONLY
	xor rdx, rdx 	;xor is the fastest way to write 0
	syscall
	mov qword [filename], rax ;after syscall %rax holds the result - name of file

length_count:
	mov rax, SYS_LSEEK
	mov rdi, [filename]
	xor rsi, rsi 
	mov rdx, SEEK_END
	syscall
	mov qword [len_text], rax

file_to_memory: ;load(map) file in memory
	mov rax, SYS_MMAP
	xor rdi, rdi
	mov rsi, [len_text]
	mov rdx, PROT_READ
	mov r10, MAP_SHARED
	mov r8, [filename]
	xor r9, r9
	syscall
	mov qword [pointer], rax

write:
	printer [pointer], [len_text]

munmap: ;terminate previously mapped memory
	mov rax, SYS_MUNMAP
	mov rdi, [pointer]
	mov rsi, [len_text]
	syscall

;close file thread
	mov rax, SYS_CLOSE
	mov rdi, [filename]
	syscall

exit:
	mov rax, SYS_EXIT
	xor rdi,rdi
	syscall

bad_exit: 
	printer bad_args_msg, bad_msg_len
	jmp exit
