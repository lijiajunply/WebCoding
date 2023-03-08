<template>
  <codemirror
      v-model="code"
      placeholder="Code gose here..."
      :style="{ height: '400px' }"
      :autofocus="true"
      :indent-with-tab="true"
      :tabSize="2"
      :extensions="extensions"
  />
  <button @click="f" >Run</button>
  <label>{{result}}</label>
</template>
<script lang="ts" setup>
import { Codemirror } from "vue-codemirror"
import { javascript } from "@codemirror/lang-javascript"
import { oneDark } from "@codemirror/theme-one-dark"
import {ref} from "vue";
import Axios from "axios"
let result = ref("")
let code = ref(`console.log('Hello, world!')`)
const extensions = ref([javascript(), oneDark])
function f() {
  Axios.post("https://127.0.0.1:5217/api/Debug",{
    "code" : "a = 1",
    "lang" : "py"
  }).then((res) => this.result = res.data
  ).catch((res) => console.log(res)
  )
}
</script>
