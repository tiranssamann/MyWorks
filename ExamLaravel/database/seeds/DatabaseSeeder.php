<?php

use App\Students;
use Illuminate\Database\Seeder;
use App\Countries;

class DatabaseSeeder extends Seeder
{
    /**
     * Seed the application's database.
     *
     * @return void
     */
    public function run()
    {

        // Create Faker Factory
        $faker = \Faker\Factory::create();

        // Insert Fake Students name, surname, age
        // for ($i=0; $i < 15; $i++) {
        //     $student = new Students();
        //     $student->name = $faker->firstNameMale;
        //     $student->surname = $faker->lastName;
        //     $student->age = $faker->numberBetween(25, 35);
        //     $student->address = $faker->address;
        //     $student->save();
        // }

        // $students = Students::all();
        // foreach ($students as $student) {
        //     $student->address = $faker->address;
        //     $student->save();
        // }

        // Insert Fake country_name and info
        for ($i=0; $i < 20; $i++) {
            $country = new Countries();
            $country->country_name = $faker->country;
            $country->info = $faker->sentence(10, true);
            $country->save();
        }

    }
}
