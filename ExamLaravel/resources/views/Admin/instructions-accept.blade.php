@extends('layouts.adm')

@section('content')
<table class="table table-hover table-dark">
    <thead>
        <tr>
            <th scope="col">#</th>
            <th scope="col">Instruction</th>
            <th scope="col">about</th>
            <th scope="col">Accept</th>
            <th scope="col">delete</th>
        </tr>
    </thead>
    <tbody>
        @foreach ($instruction_array as $instruction)
        <tr>
            <th scope="row">{{$instruction->id}}</th>
            <td>
                <div id="replybutton" class="btn4 like">
                    <span class="btn reply" style="color: white;">{{ $instruction->name }}</span>
                </div>

            </td>
            <td>
                <div id="replybutton" class="btn4 like">
                    <span class="btn reply" style="color: white;">{{ $instruction->about }}</span>
                </div>

            </td>
            <td>
                <form action="" method="POST">
                    @csrf
                    <input type="submit" value="Accept">
                    <input type="hidden" name="id" value="{{ $instruction->id }}">
                    <input type="hidden" name="action" value="accept">
                </form>
            </td>
            <td>
                <form action="" method="POST">
                    @csrf
                    <input type="submit" value="Delete">
                    <input type="hidden" name="id" value="{{ $instruction->id }}">
                    <input type="hidden" name="action" value="delete">
                </form>
            </td>
        </tr>
        @endforeach
    </tbody>
</table>

@endsection