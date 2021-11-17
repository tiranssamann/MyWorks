<!DOCTYPE html>
<html lang="{{ str_replace('_', '-', app()->getLocale()) }}">

<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">

  <!-- CSRF Token -->
  <meta name="csrf-token" content="{{ csrf_token() }}">

  <title>{{ config('app.name', 'Admin') }}</title>

  <!-- Scripts -->
  <script src="{{ asset('js/adm.js') }}" defer></script>

  <!-- Fonts -->
  <link rel="dns-prefetch" href="//fonts.gstatic.com">
  <link href="https://fonts.googleapis.com/css?family=Nunito" rel="stylesheet">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
  <!-- Styles -->
  <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css" integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh" crossorigin="anonymous">
  <link href="{{ asset('css/adm.css') }}" rel="stylesheet">
</head>

<body>
  <div id="app">
    <nav class="navbar navbar-expand-md navbar-dark bg-dark shadow-sm">
      <button class="openbtn" onclick="openNav()">☰ Open Sidebar</button>
      <div class="container">
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="{{ __('Toggle navigation') }}">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarSupportedContent">
          <!-- Left Side Of Navbar -->
          <ul class="navbar-nav mr-auto">

          </ul>
          <!-- Right Side Of Navbar -->
          <ul class="navbar-nav ml-auto">

            <li class="nav-item dropdown">
              <span style="color: white;">{{ Auth::user()->name }}</span>
            </li>
          </ul>
        </div>
      </div>
    </nav>
    <div id="mySidebar" class="sidebar">
      <a href="javascript:void(0)" class="closebtn" onclick="closeNav()">×</a>
      <a href="{{ url('/') }}">Главная</a>
      <a href="#" onClick=" $('#reply').toggle();" >Пользователи</span>
        <ul id="reply" style="display:none;">
          <li>
            <a href="{{ route('users-edit') }}">Просмотр/изменение</a>
          </li>
          <li>
            <a href="{{ route('users-add') }}">Добавить Пользователя</a>
          </li>
        </ul>
        <a href="#" onClick=" $('#replyinst').toggle();" >Инструкции</span>
          <ul id="replyinst" style="display:none;">
            <li>
              <a href="{{ route('instructions-edit') }}">Просмотр/изменение</a>
            </li>
            <li>
              <a href="{{ route('instructions-add') }}">Добавить Инструкцию</a>
            </li>
            <li>
              <a href="{{ route('instructions-accept') }}">Одобрить Инструкцию</a>
            </li>
          </ul>
          <a href="{{ route('complaints-show') }}">Жалобы</a>
    </div>

    <div id="main">

      <main class="py-4">
        @yield('content')
      </main>
    </div>
  </div>
  </div>
</body>

</html>