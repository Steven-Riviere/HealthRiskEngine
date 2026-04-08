# 🏥 Projet Médicalib – Architecture Microservices

## 📌 Description
Médicalib est une application backend développée en **.NET** basée sur une architecture **microservices** permettant de gérer des patients et d’analyser leurs risques médicaux (diabète, cancer, etc.).

L’application repose sur plusieurs APIs indépendantes (patients, notes, analyse de risques), interconnectées via une **API Gateway (Ocelot)**.

Le projet intègre également un système d’authentification et une séparation des responsabilités entre services, afin de simuler une architecture proche d’un environnement de production.

---

## ⚙️ Fonctionnalités
- Authentification sécurisée avec gestion des utilisateurs
- Gestion des patients (CRUD)
- Gestion des notes médicales associées aux patients
- Analyse du risque médical basée sur les données disponibles
- Architecture microservices découplée
- API Gateway centralisant les appels (Ocelot)
- Communication inter-services via HTTP
- Tests unitaires et tests d’intégration
- Pipeline CI/CD automatisée (GitHub Actions)

---

## 🗄️ Base de données
- **SQL Server** : utilisé pour les données principales (patients, utilisateurs, authentification).
- **MongoDB** : utilisé pour le microservice de notes médicales afin de stocker les observations liées aux patients.

---

## 🧪 Tests
- Tests unitaires pour valider la logique métier
- Tests d’intégration avec MongoDB
- Nettoyage des données entre chaque test pour garantir l’isolation
- Exécution automatique des tests dans la pipeline CI

---

## 🚀 CI/CD (GitHub Actions)
Une pipeline est déclenchée automatiquement à chaque push sur la branche principale :
- Restauration des dépendances
- Build de la solution
- Exécution des tests unitaires et d’intégration
- Utilisation d’un conteneur MongoDB pour simuler l’environnement réel

---

## 🧠 Points techniques abordés
- Architecture microservices
- API Gateway avec Ocelot
- Communication entre services via HTTP
- Gestion multi-bases de données (SQL Server + MongoDB)
- Injection de dépendances en .NET
- Tests unitaires et tests d’intégration
- Configuration via variables d’environnement
- Conteneurisation avec Docker
- Intégration continue avec GitHub Actions

---

## 🎯 Objectifs du projet
- Concevoir une architecture modulaire et évolutive
- Séparer les responsabilités entre services
- Mettre en place une communication fiable entre microservices
- Automatiser les tests via une pipeline CI/CD
- Simuler un environnement proche de la production

---

## 🛠️ Technologies utilisées
<p align="left">
  <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" />
  <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white" />
  <img src="https://img.shields.io/badge/MongoDB-47A248?style=for-the-badge&logo=mongodb&logoColor=white" />
  <img src="https://img.shields.io/badge/Ocelot-000000?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white" />
  <img src="https://img.shields.io/badge/xUnit-25A162?style=for-the-badge&logo=xunit&logoColor=white" />
  <img src="https://img.shields.io/badge/GitHub%20Actions-2088FF?style=for-the-badge&logo=githubactions&logoColor=white" />
</p>
