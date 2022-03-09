import firebase_admin
from firebase_admin import credentials
from firebase_admin import db

class fire_rd:


    cred = credentials.Certificate('-_-')

    firebase_admin.initialize_app(cred, {
        'databaseURL': '-_-', 
    })

    users_ref = db.reference()
    def fire_add(z_data):
        fire_rd.users_ref.set({
            'z': z_data   
            })
            