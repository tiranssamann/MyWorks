<?php

namespace App\Http\Controllers;

use Illuminate\Support\Facades\Hash;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\DB;
use App\InstructionModel;
use App\ComplaintsModel;
use App\User;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\File;
class AdminController extends Controller
{
    public function __construct()
    {
        $this->middleware('auth');
    }
    public function checkLogin()
    {
        if (Auth::check() && auth()->user()->is_admin == 1 && auth()->user()->is_banned == 0) {
            return view('Admin/admin');
        } else {
            return redirect('/');
        }
    }
    public function newUser(Request $request)
    {
        if (auth()->user()->is_admin == 1 && auth()->user()->is_banned == 0) {
            $action = $request->input('action');
            $nameuser = $request->input('username');
            $useremail = $request->input('email');
            $userpassword = $request->input('password');
            if ($action == 'insert') {
                if ($request->has('isadmin')) {
                    User::create([
                        'name' => $nameuser,
                        'email' => $useremail,
                        'password' => Hash::make($userpassword),
                        'is_admin' => 1,
                        'is_banned' => 0
                    ]);
                } else {
                    User::create([
                        'name' => $nameuser,
                        'email' => $useremail,
                        'password' => Hash::make($userpassword),
                        'is_admin' => 0,
                        'is_banned' => 0
                    ]);
                }
            }
            return view('Admin/users-add');
        } else if (auth()->user()->is_admin == 0 || auth()->user()->is_banned == 0 ||  auth()->user()->is_banned == 1) {
            return redirect(route('home'));
        }
    }
    public function editUsers(Request $request)
    {
        if (auth()->user()->is_admin == 1 && auth()->user()->is_banned == 0) {
            $action = $request->input('action');
            $username = $request->input('username');
            $edit_password = $request->input('edit_password');
            $edit_email = $request->input('email');
            $edit_is_admin = $request->input('edit_is_admin');
            $edit_is_banned = $request->input('edit_is_banned');

            $id = $request->input('id');
            if ($action == 'edit_name') {
                $edit_user = User::find($id);
                $edit_user->name = $username;
                $edit_user->save();
            } else if ($action == 'edit_password') {
                $edit_user = User::find($id);
                $edit_user->password = Hash::make($edit_password);
                $edit_user->save();
            } else if ($action == 'email') {
                $edit_user = User::find($id);
                $edit_user->email = $edit_email;
                $edit_user->save();
            } else if ($action == 'edit_is_admin') {
                $edit_user = User::find($id);
                $edit_user->is_admin = $edit_is_admin;
                $edit_user->save();
            } else if ($action == 'edit_is_banned') {
                $edit_user = User::find($id);
                $edit_user->is_banned = $edit_is_banned;
                $edit_user->save();
            } else if ($action == 'delete') {
                $edit_user = User::destroy($id);
            }
            $users_array = DB::select('SELECT * FROM users');
            return view('Admin/users-edit', ['users_array' => $users_array]);
        } else if (auth()->user()->is_admin == 0 || auth()->user()->is_banned == 0 ||  auth()->user()->is_banned == 1) {
            return redirect(route('home'));
        }
    }
    public function newInstruction(Request $request)
    {
        if (auth()->user()->is_admin == 1 && auth()->user()->is_banned == 0) {
            $action = $request->input('action');
            $nameInstruction = $request->input('instructionname');
            $about = $request->input('Product');
            if ($action == 'insert') {
                $file = $request->file('file');
                $filename = $nameInstruction . "_" . $about . ".pdf";
                $filePath = public_path() . '/laraview/instructions/';
                $file->move($filePath, $filename);
                if ($request->has('ispublish')) {

                    InstructionModel::create([
                        'name' => $nameInstruction,
                        'about' => $about,
                        'is_ok' => 1,
                        'filename' => $filename
                    ]);
                } else {
                    InstructionModel::create([
                        'name' => $nameInstruction,
                        'about' => $about,
                        'is_ok' => 0,
                        'filename' => $filename
                    ]);
                }
            }
            return view('Admin/instructions-add');
        } else if (auth()->user()->is_admin == 0 || auth()->user()->is_banned == 0 ||  auth()->user()->is_banned == 1) {
            return redirect(route('home'));
        }
    }
    public function editInstructions(Request $request)
    {
        if (auth()->user()->is_admin == 1 && auth()->user()->is_banned == 0) {
            $action = $request->input('action');
            $instname = $request->input('instname');
            $edit_about = $request->input('about');
            $id = $request->input('id');
            
            if ($action == 'edit_name') {
                $edit_inst = InstructionModel::find($id);
                $edit_inst->name = $instname;
                $edit_inst->save();
            } else if ($action == 'edit_about') {
                $edit_inst = InstructionModel::find($id);
                $edit_inst->about = $edit_about;
                $edit_inst->save();
            } else if ($action == 'edit_file') {
                $filename = $request->file('file');
                $edit_inst = InstructionModel::find($id);
                $file = $edit_inst->name . "_" . $edit_inst->about . ".pdf";
                $edit_inst->filename = $file;
                $edit_inst->save();
                $filePath = public_path() . '/laraview/instructions/';
                $filename->move($filePath, $file);
            } else if ($action == 'delete') {
                $edit_inst = InstructionModel::destroy($id);
            } else if ($action == 'deletefile') {
                $filePath = public_path() . '/laraview/instructions/';
                $edit_inst = InstructionModel::find($id);
                $file = $edit_inst->name . "_" . $edit_inst->about . ".pdf";
                $edit_inst->filename = "";
                $edit_inst->save();
                $filedelete = $filePath."".$file;
                File::delete($filedelete);
            }
            $instruction_array = DB::select('SELECT * FROM instruction where is_ok = 1');
            return view('Admin/instructions-edit', ['instruction_array' => $instruction_array]);
        } else if (auth()->user()->is_admin == 0 || auth()->user()->is_banned == 0 ||  auth()->user()->is_banned == 1) {
            return redirect(route('home'));
        }
    }
    public function complaints(Request $request)
    {
        if (auth()->user()->is_admin == 1 && auth()->user()->is_banned == 0) {
            $action = $request->input('action');
            $id = $request->input('id');
            if ($action == 'delete') {
                $deleteComp = ComplaintsModel::destroy($id);
            }
            $complaints_array = DB::select('SELECT * FROM complaints');
            return view('Admin/complaints-show', ['complaints_array' => $complaints_array]);
        } else if (auth()->user()->is_admin == 0 || auth()->user()->is_banned == 0 ||  auth()->user()->is_banned == 1) {
            return redirect(route('home'));
        }
    }
    public function acceptInst(Request $request)
    {
        if (auth()->user()->is_admin == 1 && auth()->user()->is_banned == 0) {
            $action = $request->input('action');
            $id = $request->input('id');
            if ($action == 'delete') {
                $deleteinst = InstructionModel::destroy($id);
            }
            if ($action == 'accept') {
                $edit_inst = InstructionModel::find($id);
                $edit_inst->is_ok = 1;
                $edit_inst->save();
            }
            $instruction_array = DB::select('SELECT * FROM instruction where is_ok = 0');
            return view('Admin/instructions-accept', ['instruction_array' => $instruction_array]);
        } else if (auth()->user()->is_admin == 0 || auth()->user()->is_banned == 0 ||  auth()->user()->is_banned == 1) {
            return redirect(route('home'));
        }
    }
}
