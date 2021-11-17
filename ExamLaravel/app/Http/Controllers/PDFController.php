<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\DB;
use App\InstructionModel;
use App\ComplaintsModel;
use App\User;

class PDFController extends Controller
{
    public function downloadpdf(Request $request){
        $idinst = $request->input('id');
        $instruction_array = InstructionModel::find($idinst);
        $filename = $instruction_array->filename;
        $filePath = public_path() . '/laraview/instructions/';
        $file = $filePath."".$filename;
        return response()->download($file); 
    }
    public function viewPDG(Request $request){
        $idinst = $request->input('id');
        $instruction_array = InstructionModel::find($idinst);
        $filename = $instruction_array->filename;
        $filePath = public_path() . '/laraview/instructions/';
        $file = $filePath."".$filename;
        return response()->file($file);  
    }
    public function viewPDGp(Request $request){
        $idinst = $request->input('id');
        $instruction_array = DB::select('SELECT * FROM instruction where id = ?',[$idinst]);
        return view('pdfreadView',['instruction_array' => $instruction_array]);  
    }
}