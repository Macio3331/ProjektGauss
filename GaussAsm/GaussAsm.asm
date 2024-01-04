.data
array oword	0000000000000009001F0030001F0009h

.code
gauss_filter_asm proc ; rcx <- adres oryginalnej tablicy, rdx <- adres modyfikowanej tablicy, r8 <- offset, r9 <- iloœæ elementów do przetworzenia

mov r10, rcx
mov r11, rdx
xor r12, r12
xor r13, r13
mov r12, 32767
mov r13, -32768
movdqu xmm5, [array]

loop1:
	dec r9
	mov rax, 0
	cmp r9, rax
	jl end_loop

	; ================

	vxorps ymm0, ymm0, ymm0
	vxorps ymm1, ymm1, ymm1
	vxorps ymm2, ymm2, ymm2
	vxorps ymm3, ymm3, ymm3
	vxorps ymm4, ymm4, ymm4
	mov rcx, 0

	movdqu xmm4, [r10 + r8]
	vpmullw ymm2, ymm4, ymm5
	vpmulhw ymm3, ymm4, ymm5
	vpunpcklwd ymm0, ymm2, ymm3
	vpunpckhwd ymm1, ymm2, ymm3
	vperm2f128 ymm1, ymm1, ymm1, 1
	vperm2f128 ymm0, ymm0, ymm1, 48
	vpsrad ymm0, ymm0, 7

	vmovq rax, xmm0
	add ecx, eax
	shr rax, 32
	add ecx, eax
	movhlps xmm0, xmm0
	vmovq rax, xmm0
	add ecx, eax
	shr rax, 32
	add ecx, eax
	vextracti128 xmm0, ymm0, 1
	vmovq rax, xmm0
	add ecx, eax

	mov edx, 0
	cmp ecx, edx
	jl negative
	cmp ecx, r12d
	cmova ecx, r12d
	jmp last_operation

negative:
	cmp ecx, r13d
	cmovb ecx, r13d

	; ================

last_operation:
	mov word ptr [r11 + r8], cx
	add r8, 2
	jmp loop1

end_loop:
	ret

gauss_filter_asm endp
end