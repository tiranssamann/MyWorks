
let search = 'tashkent';
let API_URL = `http://api.openweathermap.org/data/2.5/weather?q=${search}&appid=cda65707acd1bb69e81909ae9de1eccb`;
let API_URLdas = `http://api.openweathermap.org/data/2.5/forecast?q=${search}&appid=cda65707acd1bb69e81909ae9de1eccb`;
const btn3 = document.querySelector('.ser');
const inp = document.getElementById('sear');
btn3.addEventListener('click',function(){
    search = inp.value;
    inp.value = '';
    inp.placeholder = search;
    API_URL = `http://api.openweathermap.org/data/2.5/weather?q=${search}&appid=cda65707acd1bb69e81909ae9de1eccb`;
    API_URLdas = `http://api.openweathermap.org/data/2.5/forecast?q=${search}&appid=cda65707acd1bb69e81909ae9de1eccb`;
    getWeather2(API_URLdas);
    getWeather1(API_URL);
})
function takeerror(){
    today.innerHTML = `<div class="errors" style="justify-content: center; width: 100%;display:flex;">

            <h1 style="color: rgb(0, 165, 129); font-size: 100px;" > 4 </h1>
            <h3 style="color: rgb(0, 165, 129); font-size: 50px;" > 0 </h3>
            <h1 style="color: rgb(0, 165, 129); font-size: 100px;" > 4 </h1><br>
            
    </div>
    <div style="text-align:center;">
            <p style="color: rgb(99, 99, 99); font-size: 50px;"> ${search} could not be found. </p>
            <p style="color: rgb(99, 99, 99); font-size: 50px;"> Please enter a different location. </p>
    </div>`;
}



const btn1 = document.querySelector('.today');
const btn2 = document.querySelector('.next_days');
const now = new Date();
const today = document.getElementById('todoy');
const hor = document.getElementById('hoursd');


getWeather2(API_URLdas);
getWeather1(API_URL);

btn1.addEventListener('click', e => {

    getWeather1(API_URL);
    getWeather2(API_URLdas);
})
btn2.addEventListener('click', function () {
    getWeather2(API_URLdas);
    getWeather3(API_URLdas);
})
async function getWeather2(url) {
        const res = await fetch(url);
        const data = await res.json();
        if(data.list === undefined){
            takeerror()
            document.getElementById("myDIV").style.display = "none";
            document.getElementById("myDIV1").style.display = "none";
        }else{
            document.getElementById("myDIV").style.display = "block";
            document.getElementById("myDIV1").style.display = "block";
           inserthourtemp(data) 
        }
}
async function getWeather1(url) {
    const res = await fetch(url);
    const data = await res.json(); console.log(data.name);
    if(data.name === undefined){
        takeerror()
        document.getElementById("myDIV").style.display = "none";
        document.getElementById("myDIV1").style.display = "none";
    }else{
        document.getElementById("myDIV").style.display = "block";
        document.getElementById("myDIV1").style.display = "block";
        insertCurrentWeather(data) 
    }
    
}
async function getWeather3(url) {
    const res = await fetch(url);
    const data = await res.json();
    if(data.list === undefined){
        takeerror()
        document.getElementById("myDIV1").style.display = "none";
        document.getElementById("myDIV").style.display = "none";
    }else{
        document.getElementById("myDIV").style.display = "block";
        document.getElementById("myDIV1").style.display = "block";
        insertForecast(data) 
    }
    
}

function insertCurrentWeather(data) {
    today.innerHTML = ' ';
    const { name, main, wind, weather, sys } = data
    let a = agetHours(sys.sunrise);
    let b = agetHours(sys.sunset);
    let c = sys.sunset - sys.sunrise;
    let d = agetHours(c);
    today.innerHTML = `
    <h3>CURRENT WEATHER</h3>
    <div class="current_date">${now.getFullYear()}.${now.getMonth() + 1}.${now.getDate()}</div>
    <div class="current_main">
        <div class="current_img"><img src="http://openweathermap.org/img/w/${weather[0].icon}.png">
        <p style="margin-left:25px;"><b>${weather[0].main}</b>
        </div>
        <div class="current_degrees">
            <div class="temperature">${Math.floor(main.temp - 273)}&deg;C</div><br>
            <div class="current_realFeel">
                Real Feel ${Math.floor(main.feels_like - 273)}&deg;C
            </div>
        </div>
        <div class="current_inform">
            Sunrise: ${a.getHours()}:${a.getMinutes()}<br>
            Sunset: ${b.getHours()}:${b.getMinutes()}<br>
            Duration: ${d.getHours()}:${d.getMinutes()}
        </div>
    </div>
    `;

}
function inserthourtemp(data) {
    console.log(data);
    const { list } = data;

    let time1 = agetHours(list[0].dt);

    let time2 = agetHours(list[1].dt);
    let time3 = agetHours(list[2].dt);
    let time4 = agetHours(list[3].dt);
    let time5 = agetHours(list[4].dt);
    let time6 = agetHours(list[5].dt);

    hor.innerHTML = `
                    <table>
                        <tr>
                            <td>Today</td>
                            <td>${time1.getHours()}:${time1.getMinutes()}</td>
                            <td>${time2.getHours()}:${time2.getMinutes()}</td>
                            <td>${time3.getHours()}:${time3.getMinutes()}</td>
                            <td>${time4.getHours()}:${time4.getMinutes()}</td>
                            <td>${time5.getHours()}:${time5.getMinutes()}</td>
                            <td>${time6.getHours()}:${time6.getMinutes()}</td>
                        </tr>
                        <tr>
                            <td>.</td>
                            <td><img src="http://openweathermap.org/img/w/${list[0].weather[0].icon}.png"></td>
                            <td><img src="http://openweathermap.org/img/w/${list[1].weather[0].icon}.png"></td>
                            <td><img src="http://openweathermap.org/img/w/${list[2].weather[0].icon}.png"></td>
                            <td><img src="http://openweathermap.org/img/w/${list[3].weather[0].icon}.png"></td>
                            <td><img src="http://openweathermap.org/img/w/${list[4].weather[0].icon}.png"></td>
                            <td><img src="http://openweathermap.org/img/w/${list[5].weather[0].icon}.png"></td>
                        </tr>
                        <tr>
                            <td>Forecast</td>
                            <td>${list[0].weather[0].main}</td>
                            <td>${list[1].weather[0].main}</td>
                            <td>${list[2].weather[0].main}</td>
                            <td>${list[3].weather[0].main}</td>
                            <td>${list[4].weather[0].main}</td>
                            <td>${list[5].weather[0].main}</td>
                        </tr>
                        <tr>
                            <td>Temp</td>
                            <td>${Math.floor(list[0].main.temp - 273)}</td>
                            <td>${Math.floor(list[1].main.temp - 273)}</td>
                            <td>${Math.floor(list[2].main.temp - 273)}</td>
                            <td>${Math.floor(list[3].main.temp - 273)}</td>
                            <td>${Math.floor(list[4].main.temp - 273)}</td>
                            <td>${Math.floor(list[5].main.temp - 273)}</td>
                        </tr>
                        <tr>
                            <td>Real feal</td>
                            <td>${Math.floor(list[0].main.feels_like - 273)}</td>
                            <td>${Math.floor(list[1].main.feels_like - 273)}</td>
                            <td>${Math.floor(list[2].main.feels_like - 273)}</td>
                            <td>${Math.floor(list[3].main.feels_like - 273)}</td>
                            <td>${Math.floor(list[4].main.feels_like - 273)}</td>
                            <td>${Math.floor(list[5].main.feels_like - 273)}</td>
                        </tr>
                        <tr>
                        <td>Wind (km/h)</td>
                        <td>${list[0].wind.speed }</td>
                        <td>${list[1].wind.speed }</td>
                        <td>${list[2].wind.speed }</td>
                        <td>${list[3].wind.speed }</td>
                        <td>${list[4].wind.speed }</td>
                        <td>${list[5].wind.speed }</td>
                    </tr>
                    </table>
    `;

}


function agetHours(timestamp) {

    return new Date(+timestamp * 1000)
}

async function insertForecast(data) {
    today.innerHTML = ' '

    const { list } = data;
    let day2 = new Date(now);
    day2.setDate(day2.getDate() + 1);
    let day3 = new Date(day2);
    day3.setDate(day3.getDate() + 1);
    let day4 = new Date(day3);
    day4.setDate(day4.getDate() + 1);
    let day5 = new Date(day4);
    day5.setDate(day5.getDate() + 1);
    const API_URLdass = `https://api.openweathermap.org/data/2.5/onecall?lat=${data.city.coord.lat}&lon=${data.city.coord.lon}&appid=cda65707acd1bb69e81909ae9de1eccb`;
    const res1 = await fetch(API_URLdass);
    const data1 = await res1.json();


    today.innerHTML = `
    <div class="fivedays" id="fivedays">


    <div class="day" id="days1">
    <p id="day1" style="color: rgb(0, 165, 129);">${now.toLocaleString('en', { weekday: 'long' })}</p>
    <p  style="color: black;">${now.toLocaleString('en', { month: 'short' })} ${now.getDate()}</p>
		<img src="http://openweathermap.org/img/w/${data1.daily[1].weather[0].icon}.png"></img><br>
		<p  style="color: black; font-size: 40px;">${Math.floor( data1.daily[1].temp.day - 273)}&deg;C</p>
		<p  style="color: black; ">${ data1.daily[1].weather[0].description}</p>

	</div>

    <div class="day" id="days2">
    <p id="day1" style="color: rgb(0, 165, 129);">${day2.toLocaleString('en', { weekday: 'long' })}</p>
    <p  style="color: black;">${day2.toLocaleString('en', { month: 'short' })} ${day2.getDate()}</p>
    <img src="http://openweathermap.org/img/w/${data1.daily[2].weather[0].icon}.png"></img><br>
		<p  style="color: black; font-size: 40px;">${Math.floor( data1.daily[2].temp.day - 273)}&deg;C</p>
		<p  style="color: black; ">${ data1.daily[2].weather[0].description}</p>
	</div>

    <div class="day" id="days3">
    <p id="day1" style="color: rgb(0, 165, 129);">${day3.toLocaleString('en', { weekday: 'long' })}</p>
    <p  style="color: black;">${day3.toLocaleString('en', { month: 'short' })} ${day3.getDate()}</p>
    <img src="http://openweathermap.org/img/w/${data1.daily[3].weather[0].icon}.png"></img><br>
		<p  style="color: black; font-size: 40px;">${Math.floor( data1.daily[3].temp.day - 273)}&deg;C</p>
		<p  style="color: black; ">${ data1.daily[3].weather[0].description}</p>
	</div>

    <div class="day" id="days4">
    <p id="day1" style="color: rgb(0, 165, 129);">${day4.toLocaleString('en', { weekday: 'long' })}</p>
    <p  style="color: black;">${day4.toLocaleString('en', { month: 'short' })} ${day4.getDate()}</p>
    <img src="http://openweathermap.org/img/w/${data1.daily[4].weather[0].icon}.png"></img><br>
		<p  style="color: black; font-size: 40px;">${Math.floor( data1.daily[4].temp.day - 273)}&deg;C</p>
		<p  style="color: black; ">${ data1.daily[4].weather[0].description}</p>
	</div>
    <div class="day" id="days5">
    <p id="day1" style="color: rgb(0, 165, 129);">${day5.toLocaleString('en', { weekday: 'long' })}</p>
    <p  style="color: black;">${day5.toLocaleString('en', { month: 'short' })} ${day5.getDate()}</p>
    <img src="http://openweathermap.org/img/w/${data1.daily[5].weather[0].icon}.png"></img><br>
		<p  style="color: black; font-size: 40px;">${Math.floor( data1.daily[5].temp.day - 273)}&deg;C</p>
		<p  style="color: black; ">${ data1.daily[5].weather[0].description}</p>
	</div>
</div>


    `

    const days1 = document.getElementById("days1");
const days2 = document.getElementById("days2");
const days3 = document.getElementById("days3");
const days4 = document.getElementById("days4");
const days5 = document.getElementById("days5");
if(days1){

days1.addEventListener('click',function () {
    days1.style.backgroundColor = 'white';
    days2.style.backgroundColor = 'rgb(226, 226, 226)';
    days3.style.backgroundColor = 'rgb(226, 226, 226)';
    days4.style.backgroundColor = 'rgb(226, 226, 226)';
    days5.style.backgroundColor = 'rgb(226, 226, 226)';
    let dt = [];
    
    for(let i=0; i < data.cnt;i++){
        let dt1 = agetHours( data.list[i].dt)
        if(dt1.getDate() === now.getDate()){
            dt.push(data.list[i])
        }
    }
    console.log(dt);
    inserthourtemp2(dt)
})
days2.addEventListener('click',function () {
    days2.style.backgroundColor = 'white';
    days1.style.backgroundColor = 'rgb(226, 226, 226)';
    days3.style.backgroundColor = 'rgb(226, 226, 226)';
    days4.style.backgroundColor = 'rgb(226, 226, 226)';
    days5.style.backgroundColor = 'rgb(226, 226, 226)';
    let dt = [];
    
    for(let i=0; i < data.cnt;i++){
        let dt1 = agetHours( data.list[i].dt)
        if(dt1.getDate() === day2.getDate()){
            dt.push(data.list[i])
        }
    }
    console.log(dt);
    inserthourtemp2(dt)
})
days3.addEventListener('click',function () {
    days3.style.backgroundColor = 'white';
    days2.style.backgroundColor = 'rgb(226, 226, 226)';
    days1.style.backgroundColor = 'rgb(226, 226, 226)';
    days4.style.backgroundColor = 'rgb(226, 226, 226)';
    days5.style.backgroundColor = 'rgb(226, 226, 226)';
    let dt = [];
    
    for(let i=0; i < data.cnt;i++){
        let dt1 = agetHours( data.list[i].dt)
        if(dt1.getDate() === day3.getDate()){
            dt.push(data.list[i])
        }
    }
    console.log(dt);
    inserthourtemp2(dt)
})
days4.addEventListener('click',function () {
    days4.style.backgroundColor = 'white';
    days2.style.backgroundColor = 'rgb(226, 226, 226)';
    days3.style.backgroundColor = 'rgb(226, 226, 226)';
    days1.style.backgroundColor = 'rgb(226, 226, 226)';
    days5.style.backgroundColor = 'rgb(226, 226, 226)';
    let dt = [];
    
    for(let i=0; i < data.cnt;i++){
        let dt1 = agetHours( data.list[i].dt)
        if(dt1.getDate() === day4.getDate()){
            dt.push(data.list[i])
        }
    }
    console.log(dt);
    inserthourtemp2(dt)
})
days5.addEventListener('click',function () {
    days5.style.backgroundColor = 'white';
    days2.style.backgroundColor = 'rgb(226, 226, 226)';
    days3.style.backgroundColor = 'rgb(226, 226, 226)';
    days4.style.backgroundColor = 'rgb(226, 226, 226)';
    days1.style.backgroundColor = 'rgb(226, 226, 226)';
    let dt = [];
    
    for(let i=0; i < data.cnt;i++){
        let dt1 = agetHours( data.list[i].dt)
        if(dt1.getDate() === day5.getDate()){
            dt.push(data.list[i])
        }
    }
    console.log(dt);
    inserthourtemp2(dt)
})}
}

function inserthourtemp2(list) {

    let time1 = agetHours(list[0].dt);

    let time2 = agetHours(list[1].dt);
    let time3 = agetHours(list[2].dt);
    let time4 = agetHours(list[3].dt);
    let time5 = agetHours(list[4].dt);
    let time6 = agetHours(list[5].dt);

    hor.innerHTML = `
                    <table>
                        <tr>
                            <td>Today</td>
                            <td>${time1.getHours()}:${time1.getMinutes()}</td>
                            <td>${time2.getHours()}:${time2.getMinutes()}</td>
                            <td>${time3.getHours()}:${time3.getMinutes()}</td>
                            <td>${time4.getHours()}:${time4.getMinutes()}</td>
                            <td>${time5.getHours()}:${time5.getMinutes()}</td>
                            <td>${time6.getHours()}:${time6.getMinutes()}</td>
                        </tr>
                        <tr>
                            <td>.</td>
                            <td><img src="http://openweathermap.org/img/w/${list[0].weather[0].icon}.png"></td>
                            <td><img src="http://openweathermap.org/img/w/${list[1].weather[0].icon}.png"></td>
                            <td><img src="http://openweathermap.org/img/w/${list[2].weather[0].icon}.png"></td>
                            <td><img src="http://openweathermap.org/img/w/${list[3].weather[0].icon}.png"></td>
                            <td><img src="http://openweathermap.org/img/w/${list[4].weather[0].icon}.png"></td>
                            <td><img src="http://openweathermap.org/img/w/${list[5].weather[0].icon}.png"></td>
                        </tr>
                        <tr>
                            <td>Forecast</td>
                            <td>${list[0].weather[0].main}</td>
                            <td>${list[1].weather[0].main}</td>
                            <td>${list[2].weather[0].main}</td>
                            <td>${list[3].weather[0].main}</td>
                            <td>${list[4].weather[0].main}</td>
                            <td>${list[5].weather[0].main}</td>
                        </tr>
                        <tr>
                            <td>Temp</td>
                            <td>${Math.floor(list[0].main.temp - 273)}</td>
                            <td>${Math.floor(list[1].main.temp - 273)}</td>
                            <td>${Math.floor(list[2].main.temp - 273)}</td>
                            <td>${Math.floor(list[3].main.temp - 273)}</td>
                            <td>${Math.floor(list[4].main.temp - 273)}</td>
                            <td>${Math.floor(list[5].main.temp - 273)}</td>
                        </tr>
                        <tr>
                            <td>Real feal</td>
                            <td>${Math.floor(list[0].main.feels_like - 273)}</td>
                            <td>${Math.floor(list[1].main.feels_like - 273)}</td>
                            <td>${Math.floor(list[2].main.feels_like - 273)}</td>
                            <td>${Math.floor(list[3].main.feels_like - 273)}</td>
                            <td>${Math.floor(list[4].main.feels_like - 273)}</td>
                            <td>${Math.floor(list[5].main.feels_like - 273)}</td>
                        </tr>
                        <tr>
                        <td>Wind (km/h)</td>
                        <td>${list[0].wind.speed }</td>
                        <td>${list[1].wind.speed }</td>
                        <td>${list[2].wind.speed }</td>
                        <td>${list[3].wind.speed }</td>
                        <td>${list[4].wind.speed }</td>
                        <td>${list[5].wind.speed }</td>
                    </tr>
                    </table>
    `;

}