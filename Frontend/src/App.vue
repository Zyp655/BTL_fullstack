<template>
  <v-app>
    <v-app-bar color="primary" elevation="1">
      <v-app-bar-title>Training Center</v-app-bar-title>
      <v-btn to="/" variant="text">Trang chu</v-btn>
      <v-btn to="/courses" variant="text">Khoa hoc</v-btn>
    </v-app-bar>

    <v-main>
      <router-view />
    </v-main>
  </v-app>
</template>
