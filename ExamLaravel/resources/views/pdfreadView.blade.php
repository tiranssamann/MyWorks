@extends('layouts.app')

@section('content')
<div class="container">
@foreach ($instruction_array as $instruction)

 <iframe src="http://examlaravel/laraview/instructions/{{$instruction->filename}}" frameborder="0" width="100%" height="1000px"></iframe>

 @endforeach
 </div>
@endsection