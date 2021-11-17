@extends('layouts.adm')

@section('content')
<table class="table table-hover table-dark">
    <thead>
        <tr>
            <th scope="col">#</th>
            <th scope="col">Complaint</th>
            <th scope="col">id_instruction</th>
            <th scope="col">id_user</th>
            <th scope="col">delete</th>
        </tr>
    </thead>
    <tbody>
        @foreach ($complaints_array as $complaints)
        <tr>
            <th scope="row">{{$complaints->id}}</th>
            <td>
                <div id="replybutton" class="btn4 like">
                    <span class="btn reply" style="color: white;">{{ $complaints->complaint }}</span>
                </div>

            </td>
            <td>
                <div id="replybutton" class="btn4 like">
                    <span class="btn reply" style="color: white;">{{ $complaints->id_instruction }}</span>
                </div>

            </td>
            <td>
                <div id="replybutton" class="btn4 like">
                    <span class="btn reply" style="color: white;">{{ $complaints->id_user }}</span>
                </div>

            </td>
            <td>
                <form action="" method="POST">
                    @csrf
                    <input type="submit" value="Delete">
                    <input type="hidden" name="id" value="{{ $complaints->id }}">
                    <input type="hidden" name="action" value="delete">
                </form>
            </td>
        </tr>
        @endforeach
    </tbody>
</table>

@endsection