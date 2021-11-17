<?php

namespace App\Http\Controllers;

use Illuminate\Support\Facades\Hash;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\DB;
use App\InstructionModel;
use App\ComplaintsModel;
use App\User;
use Illuminate\Support\Facades\Auth;

class UserController extends Controller
{
    public function complaintsUser(Request $request)
    {
        if (Auth::check() && auth()->user()->is_banned == 0) {
            $idinst = $request->input('id');
            $idus = $request->input('userid');
            $comp = $request->input('complaint');
            $action = $request->input('action');
            if ($action == 'link') {
                return view('complaintUs');
            }
            if ($action == 'postdata') {
                $query = DB::insert('insert into complaints (complaint,id_instruction,id_user) values (?, ?, ?)', [$comp, $idinst, $idus]);
            }
        } else {
            return redirect('/');
        }
        return redirect('/');
    }
    public function addinstr(Request $request)
    {
        if (Auth::check() && auth()->user()->is_banned == 0) {
            $action = $request->input('action');
            $nameInstruction = $request->input('instructionname');
            $about = $request->input('Product');
            if ($action == 'insert') {
                $file = $request->file('file');
                $filename = $nameInstruction . "_" . $about . ".pdf";
                $filePath = public_path() . '/laraview/instructions/';
                $file->move($filePath, $filename);

                InstructionModel::create([
                    'name' => $nameInstruction,
                    'about' => $about,
                    'is_ok' => 0,
                    'filename' => $filename
                ]);
            }
            return view('add-instruction');
        } else {
            return redirect('/');
        }
    }
    public function vieworder(Request $request)
    {
        $action = $request->input('action');
        $idinst = $request->input('id');
        $idus = $request->input('userid');
        if (Auth::check() && auth()->user()->is_banned == 0) {
            if ($action == 'link') {
                $query = DB::insert('insert into orders (id_instruction,id_user) values ( ?, ?)', [intval($idinst), intval($idus)]);
            }
            return view('vieworder');
        }
        else {
            return redirect('/');
        }
        
    }
}
