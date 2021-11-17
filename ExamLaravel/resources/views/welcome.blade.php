@extends('layouts.app')

@section('content')
@guest
<div class="container">
  <div class="card-columns">
    @foreach ($instruction_array as $instruction)
    <div class="card">
      <img class="card-img-top" src="/laraview/images/pdfFile.jpg" alt="Card image cap">
      <div class="card-body">
        <h5 class="card-title">{{$instruction->name}}</h5>
        <p class="card-text">{{$instruction->about}}</p>
        <div class="btn-group">
          <h1><a href="{{route('pdfread')}}" class="btn btn-secondary mr-2" onclick="event.preventDefault();
                                                     document.getElementById('{{$instruction->name}}{{ $instruction->id }}').submit();">Просмотр</a></h1>

          <form id="{{$instruction->name}}{{ $instruction->id }}" action="{{route('pdfread')}}" method="get" style="display: none;">
            <input type="hidden" name="id" value="{{ $instruction->id }}">
          </form>
          <h1><a href="{{route('pdfread/download')}}" class="btn btn-secondary mr-2" onclick="event.preventDefault();
                                                     document.getElementById('{{$instruction->name}}{{ $instruction->id }}{{ $instruction->id }}').submit();">скачать</a></h1>

          <form id="{{$instruction->name}}{{ $instruction->id }}{{ $instruction->id }}" action="{{route('pdfread/download')}}" method="get" style="display: none;">
            <input type="hidden" name="id" value="{{ $instruction->id }}">

          </form>
        </div>
      </div>
    </div>
    @endforeach
  </div>
</div>
@else
@if(Auth::user()->is_banned == 1)
<div class="alert alert-danger" role="alert">
  <center>
    <h1><a href="{{ route('logout') }}" onclick="event.preventDefault();
                                                     document.getElementById('logout-form').submit();">вы забанены!</a></h1>

    <form id="logout-form" action="{{ route('logout') }}" method="POST" style="display: none;">
      @csrf
    </form>
  </center>
</div>
@endif
@if(Auth::user()->is_banned == 0)
<div class="container">
  <div class="card-columns">
    @foreach ($instruction_array as $instruction)
    <div class="card">
      <img class="card-img-top" src="/laraview/images/pdfFile.jpg" alt="Card image cap">
      <div class="card-body">
        <h5 class="card-title">{{$instruction->name}}</h5>
        <p class="card-text">{{$instruction->about}}</p>
        <div class="btn-group">
        
          <h1><a href="pdfread?id={{$instruction->id}}" class="card-text btn btn-secondary mr-2" onclick="event.preventDefault();
                                                     document.getElementById('{{$instruction->name}}{{ $instruction->id }}').submit();">Просмотр</a></h1>

          <form id="{{$instruction->name}}{{ $instruction->id }}" action="{{route('pdfread')}}" method="get" style="display: none;">
            <input type="hidden" name="id" value="{{ $instruction->id }}">

          </form>
          <h1><a href="pdfreadView?id={{$instruction->id}}" class="card-text btn btn-secondary mr-2" onclick="event.preventDefault();
                                                     document.getElementById('{{$instruction->name}}').submit();">Просмотр на сайте</a></h1>

          <form id="{{$instruction->name}}" action="{{route('pdfreadView')}}" method="get" style="display: none;">
            <input type="hidden" name="id" value="{{ $instruction->id }}">

          </form>

        </div>
        <div class="btn-group">
          <h1><a href="{{route('pdfread/download')}}" class="card-text btn btn-secondary mr-2" onclick="event.preventDefault();
                                                     document.getElementById('{{$instruction->name}}{{ $instruction->id }}{{ $instruction->id }}').submit();">скачать</a></h1>

          <form id="{{$instruction->name}}{{ $instruction->id }}{{ $instruction->id }}" action="{{route('pdfread/download')}}" method="get" style="display: none;">
            <input type="hidden" name="id" value="{{ $instruction->id }}">

          </form>
          <h1><a href="complaintUs?id={{ $instruction->id }}&userid={{ Auth::id() }}&action=link" class="card-text btn btn-danger mr-2" onclick="event.preventDefault();
                                                     document.getElementById('{{$instruction->name}}{{ $instruction->id }}{{ $instruction->id }}{{$instruction->name}}').submit();">Пожаловаться</a></h1>

          <form id="{{$instruction->name}}{{ $instruction->id }}{{ $instruction->id }}{{$instruction->name}}" action="{{route('complaintUs')}}" method="get" style="display: none;">
            <input type="hidden" name="id" value="{{ $instruction->id }}">
            <input type="hidden" name="userid" value="{{ Auth::id() }}">
            <input type="hidden" name="action" value="link">
          </form>
        </div>
        <div class="btn-group">
        <a href="{{route('vieworder')}}" class="card-text btn btn-secondary mr-2" onclick="event.preventDefault();
                                                     document.getElementById('{{$instruction->name}}{{ $instruction->id }}{{ $instruction->id }}{{$instruction->name}}{{ $instruction->id }}').submit();">Заказать</a>
        <form id="{{$instruction->name}}{{ $instruction->id }}{{ $instruction->id }}{{$instruction->name}}{{ $instruction->id }}" action="{{route('vieworder')}}" method="post" style="display: none;">
            <input type="hidden" name="id" value="{{ $instruction->id }}">
            <input type="hidden" name="userid" value="{{ Auth::id() }}">
            <input type="hidden" name="action" value="link">
          </form>
        </div>
      </div>
    </div>
    @endforeach
  </div>
</div>
@endif
@endguest
@endsection