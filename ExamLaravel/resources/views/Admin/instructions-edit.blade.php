@extends('layouts.adm')

@section('content')
<table class="table table-hover table-dark">
    <thead>
        <tr>
            <th scope="col">#</th>
            <th scope="col">Name instruction</th>
            <th scope="col">about</th>
            <th scope="col">file</th>
            <th scope="col">delete</th>
        </tr>
    </thead>
    <tbody>
        @foreach ($instruction_array as $instruction)
        <tr>
            <th scope="row">{{$instruction->id}}</th>
            <td>
                <div id="replybutton" class="btn4 like">
                    <span class="btn reply" onClick="$('#reply{{ $instruction->id }}').toggle(); $('.reply{{ $instruction->id }}').toggle();" style="color: white;">{{ $instruction->name }}</span>
                </div>
                <form action="" method="post">
                    <input type="text" name="instname" id="reply{{ $instruction->id }}" style="display:none;">
                    <input type="submit" class="reply{{ $instruction->id }}" value="Edit" style="display:none;">
                    <input type="hidden" name="id" value="{{ $instruction->id }}">
                    <input type="hidden" name="action" value="edit_name">

                </form>
            </td>
            <td>
                <div id="replybutton" class="btn4 like">
                    <span class="btn reply" onClick="$('#reply{{$instruction->about}}{{$instruction->id}}').toggle(); $('.reply{{$instruction->about}}{{$instruction->id}}').toggle();" style="color: white;">{{ $instruction->about }}</span>
                </div>
                <form action="" method="post">
                    <input type="text" name="about" id="reply{{$instruction->about}}{{$instruction->id}}" style="display:none;">
                    <input type="submit" class="reply{{$instruction->about}}{{$instruction->id}}" value="Edit" style="display:none;">
                    <input type="hidden" name="id" value="{{ $instruction->id }}">
                    <input type="hidden" name="action" value="edit_about">

                </form>
            </td>
            <td>
                <div id="replybutton" class="btn4 like">
                    <span class="btn reply" onClick="$('#replyfile{{$instruction->id}}').toggle(); $('.replyfile{{$instruction->id}}').toggle();" style="color: white;">{{$instruction->filename}}</span>
                </div>
                <form action="" method="post" enctype="multipart/form-data">
                    <input type="file" name="file" id="replyfile{{$instruction->id}}" style="display:none;">
                    <input type="submit" class="replyfile{{$instruction->id}}" value="Edit" style="display:none;">
                    <input type="hidden" name="id" value="{{ $instruction->id }}">
                    <input type="hidden" name="action" value="edit_file">
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