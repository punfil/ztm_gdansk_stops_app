<script setup>
defineProps(['modelValue'])
defineEmits(['update:modelValue'])
</script>

<template>
    <input type="password" :value="modelValue"
           @input="$emit('update:modelValue', $event.target.value)" />
</template>