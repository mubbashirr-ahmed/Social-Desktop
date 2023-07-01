import sys
import facebook as fb

# Read command-line arguments
access_token = sys.argv[1]
image_path = sys.argv[2]

try:
    # Initialize Facebook Graph API
    asabf = fb.GraphAPI(access_token)

    # Post message
    asabf.put_object("me", "feed", message="Message post this")

    # Upload and post photo
    asabf.put_photo(open(image_path, "rb"), message="second")

    # Return "OK" if successful
    print("OK")
except Exception as e:
    # Return "Not OK" and the error message
    print(f"Not OK: {str(e)}")
